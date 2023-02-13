using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using NNDIP.ApiClient;
using NNDIP.Maui.Models.Sensor;
using NNDIP.Maui.Services;
using NNDIP.Maui.Views.Startup;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace NNDIP.Maui.ViewModels.Dashboard
{
    public partial class DashboardTestViewModel : BaseViewModel
    {
        public ISeries[] ChartSeries { get => chartSeries; set => chartSeries = value; }

        private ISeries[] chartSeries;

        private ICollection<DataDto> _data;
        public async void Load()
        {
            try
            {
                _data = await RestService.API.ApiDataHistoricalGetAsync(1, new DateTimeOffset(new DateTime(2022, 10, 6)), new DateTimeOffset(new DateTime(2022, 10, 7)));

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
        }

        [RelayCommand]
        async void Loadd()
        {
            Load();
        }
    }
}
