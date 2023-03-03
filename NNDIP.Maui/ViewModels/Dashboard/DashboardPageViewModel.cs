using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using NNDIP.ApiClient;
using NNDIP.Maui.Controls;
using NNDIP.Maui.Models;
using NNDIP.Maui.Models.Sensor;
using NNDIP.Maui.Services;
using NNDIP.Maui.Views.Startup;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NNDIP.Maui.ViewModels.Dashboard;

public partial class DashboardPageViewModel : BaseViewModel
{
    #region Properites
    [ObservableProperty]
    private AddressStateResult _addressStateResult;

    [ObservableProperty]
    private ObservableCollection<SensorsDataDto> _sensorsData;

    [ObservableProperty]
    private ObservableCollection<SensorGroup> _sensorGroups;

    [ObservableProperty]
    private bool _isRefreshing;

    [ObservableProperty]
    private DateTime? _datePickerDate;

    private ICollection<DataDto> _data;
    #endregion

    public DashboardPageViewModel()
    {
        Shell.Current.FlyoutHeader = new FlyoutHeaderControl();
        SensorGroups = new ObservableCollection<SensorGroup>();
    }

    public async void Load()
    {
        try
        {
            AddressStateResult = await RestService.API.ApiAddressStateResultsAsync();
            SensorsData = new ObservableCollection<SensorsDataDto>(await RestService.API.ApiSensorDataAsync());
            _data = await RestService.API.ApiDataHistoricalGetAsync(1, new DateTimeOffset(new DateTime(2022, 10, 6)), new DateTimeOffset(new DateTime(2022, 10, 7)));
        }
        catch (ApiClientException ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            if (ex.StatusCode == 401)
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            return;
        }
        IEnumerable<SensorGroup> groupedData = SensorsData.Select(sensor => new SensorGroup(new Sensor()
        {
            Id = sensor.Id,
            Name = sensor.Name,
            SensorType = sensor.SensorType,
            DataTimestamp = sensor.DataTimestamp
        }, sensor.Data.ToList()));
        foreach (var item in groupedData)
        {
            if (!SensorGroups.Any(groupedData => groupedData.Sensor.Id == item.Sensor.Id))
            {
                SensorGroups.Add(item);
            }
        }
    }
    public void SetDefault()
    {
        DatePickerDate = DateTime.Today;
    }

    #region Commands
    [RelayCommand]
    void Refresh()
    {
        Load();
        IsRefreshing = false;
    }

    public ICommand AddOrRemoveGroupDataCommand => new Command<SensorGroup>((sensorGroup) =>
    {
        if (sensorGroup.GroupIcon == "down_arrow.png")
        {
            sensorGroup.Clear();
            sensorGroup.GroupIcon = "up_arrow.png";
        }
        else
        {
            ICollection<SimpleDataDto> recordsTobeAdded = SensorsData.Where(sensor => sensor.Id == sensorGroup.Sensor.Id).Select(item => item.Data).FirstOrDefault();
            sensorGroup.AddRange(recordsTobeAdded);
            sensorGroup.GroupIcon = "down_arrow.png";
        }
    });

    [RelayCommand]
    void Plus()
    {
        DatePickerDate = DatePickerDate is null ? DateTime.Today.AddDays(1) : DatePickerDate.Value.AddDays(1);
    }

    [RelayCommand]
    void Minus()
    {
        DatePickerDate = DatePickerDate is null ? DateTime.Today.AddDays(-1) : DatePickerDate.Value.AddDays(-1);
    }
    #endregion
}