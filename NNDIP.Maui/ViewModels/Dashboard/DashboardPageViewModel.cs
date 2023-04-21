using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using NNDIP.ApiClient;
using NNDIP.Maui.Controls;
using NNDIP.Maui.Models;
using NNDIP.Maui.Models.Data;
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

    [ObservableProperty]
    private ObservableCollection<DataDto> _data;

    [ObservableProperty]
    private Sensor _selectedSensor;

    [ObservableProperty]
    private ObservableCollection<Sensor> _sensors = new ObservableCollection<Sensor>();

    [ObservableProperty]
    private bool _isChartLoading;

    [ObservableProperty]
    private ObservableCollection<Unit> _units = new ObservableCollection<Unit>();

    [ObservableProperty]
    private Unit _selectedUnit;

    [ObservableProperty]
    private string _typeName;

    [ObservableProperty]
    private string _unitMeasTitle;
    private bool CallDataRefresh { get; set; } = true;
    #endregion

    public DashboardPageViewModel()
    {
        Shell.Current.FlyoutHeader = new FlyoutHeaderControl();
        SensorGroups = new ObservableCollection<SensorGroup>();
    }

    public async Task Load()
    {
        try
        {
            AddressStateResult = await RestService.API.ApiAddressStateResultsAsync();
            SensorsData = new ObservableCollection<SensorsDataDto>(await RestService.API.ApiSensorDataAsync());
        }
        catch (ApiClientException ex)
        {
            await ExceptionHandlingService.HandleException(ex);
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
#if IOS
                await Task.Delay(150);
#endif
                SensorGroups.Add(item);
            }
            if (!Sensors.Any(sensor => sensor.Id == item.Sensor.Id))
            {
                Sensors.Add(item.Sensor);
            }
        }
        SetUnits();
        SelectedUnit ??= Units.FirstOrDefault();
        if (SelectedSensor is null)
        {
            CallDataRefresh = false;
            SelectedSensor = Sensors.FirstOrDefault();
            CallDataRefresh = true;
        }
        await RefreshData();
    }

    private void SetUnits()
    {
        foreach (var sensor in SensorsData)
        {
            foreach (var data in sensor.Data)
            {
                if (!Units.Any(unit => unit.TypeName.ToUpper() == data.TypeName.ToUpper()))
                {
                    Units.Add(new Unit()
                    {
                        TypeName = char.ToUpper(data.TypeName.First()) + data.TypeName.Substring(1).ToLower(),
                        UnitMeas = data.UnitMeas
                    });
                }
            }
        }
    }

    public void SetDefault()
    {
        if (DatePickerDate is null)
        {
            CallDataRefresh = false;
            DatePickerDate = DateTime.Today;
            CallDataRefresh = true;
        }
    }

    public async Task RefreshData()
    {
        if (CallDataRefresh && SelectedSensor is not null)
        {
            IsChartLoading = true;
            Data = new ObservableCollection<DataDto>(await RestService.API.ApiDataHistoricalGetAsync(SelectedSensor.Id, new DateTimeOffset(DatePickerDate.Value), new DateTimeOffset(DatePickerDate.Value.AddDays(1))));
            IsChartLoading = false;
        }
    }

    #region Commands
    [RelayCommand]
    async void Refresh()
    {
        await Load();
        IsRefreshing = false;
    }

    public ICommand AddOrRemoveGroupDataCommand => new Command<SensorGroup>(async (sensorGroup) =>
    {
        if (sensorGroup.GroupIcon == Icons.DownArrow)
        {
            sensorGroup.Clear();
            sensorGroup.GroupIcon = Icons.UpArrow;
        }
        else
        {
            ICollection<SimpleDataDto> recordsTobeAdded = SensorsData.Where(sensor => sensor.Id == sensorGroup.Sensor.Id).Select(item => item.Data).FirstOrDefault();
            sensorGroup.AddRange(recordsTobeAdded);
            sensorGroup.GroupIcon = Icons.DownArrow;
        }
    });

    [RelayCommand]
    async void Plus()
    {
        DatePickerDate = DatePickerDate is null ? DateTime.Today.AddDays(1) : DatePickerDate.Value.AddDays(1);

        await RefreshData();
    }

    [RelayCommand]
    async void Minus()
    {
        DatePickerDate = DatePickerDate is null ? DateTime.Today.AddDays(-1) : DatePickerDate.Value.AddDays(-1);

        await RefreshData();
    }

    [RelayCommand]
    async void DateSelected()
    {
        await RefreshData();
    }

    [RelayCommand]
    async void SensorSelectedIndexChanged()
    {
        await RefreshData();
    }

    [RelayCommand]
    void UnitSelectedIndexChanged()
    {
        TypeName = char.ToUpper(SelectedUnit.TypeName.First()) + SelectedUnit.TypeName.Substring(1).ToLower();
        UnitMeasTitle = SelectedUnit.UnitMeas;
    }
    #endregion
}