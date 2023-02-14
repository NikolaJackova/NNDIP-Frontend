using NNDIP.ApiClient;
using NNDIP.Maui.Models.Sensor;
using NNDIP.Maui.Services;
using NNDIP.Maui.Views.Startup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using NNDIP.Maui.Models.Data;

namespace NNDIP.Maui.ViewModels.Dashboard
{
    public partial class DashboardTestViewModel : BaseViewModel
    {
        public ObservableCollection<Data> ChartData { get; set; }
        private ICollection<DataDto> _data;
        public async void Load()
        {
            try
            {
                _data = await RestService.API.ApiDataHistoricalGetAsync(1, new DateTimeOffset(new DateTime(2022, 10, 6)), new DateTimeOffset(new DateTime(2022, 10, 7)));
                List<DataDto> dataDtos = _data.Where(item => item.Temperature is not null).ToList();
                ChartData = new ObservableCollection<Data>();
                foreach (var item in dataDtos)
                {
                    ChartData.Add(new Data()
                    {
                        Value = item.Temperature.Value,
                        Time = new DateTime(item.DataTimestamp.Year, item.DataTimestamp.Month, item.DataTimestamp.Day, item.DataTimestamp.Hour, item.DataTimestamp.Minute, 0)
                    });
                }
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
        void Loadd()
        {
            Load();
        }
    }
}
