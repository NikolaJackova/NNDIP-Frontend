using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using Newtonsoft.Json;
using NNDIP.ApiClient;
using NNDIP.Maui.Controls;
using NNDIP.Maui.Models;
using NNDIP.Maui.Models.Sensor;
using NNDIP.Maui.Services;
using NNDIP.Maui.Views.Startup;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static Android.Content.ClipData;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace NNDIP.Maui.ViewModels.Dashboard;

public partial class DashboardPageViewModel : BaseViewModel
{
    #region Properites
    [ObservableProperty]
    private AddressStateResultDto _addressStateResult;

    [ObservableProperty]
    private ObservableCollection<SensorsDataDto> _sensorsData;

    [ObservableProperty]
    private ObservableCollection<SensorGroup> _sensorGroups;

    private ISeries[] chartSeries;

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
            _data = await RestService.API.ApiDataHistoricalGetAsync(1, new DateTimeOffset(new DateTime(2022,10,6)), new DateTimeOffset(new DateTime(2022, 10, 7)));

            ChartSeries = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = _data.Where(item => item.Temperature is not null).Select(item => item.Temperature.Value).ToList(),
                    Fill = new SolidColorPaint(SKColors.AliceBlue),
                    Stroke = new SolidColorPaint(SKColors.Black, 3)
                }
            };
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
        IEnumerable<SensorGroup> groupedData = SensorsData.Select(sensor => new SensorGroup (new Sensor() 
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
    #region Commands
    [RelayCommand]
    async void Loadd()
    {
        Load();
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

    public ISeries[] ChartSeries { get => chartSeries; set => chartSeries = value; }
    #endregion
}