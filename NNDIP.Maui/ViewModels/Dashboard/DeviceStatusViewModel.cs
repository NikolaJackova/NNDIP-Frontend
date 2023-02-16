using CommunityToolkit.Mvvm.ComponentModel;
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

namespace NNDIP.Maui.ViewModels.Dashboard
{
    public partial class DeviceStatusViewModel : BaseViewModel
    {
        #region Properites
        [ObservableProperty]
        private AddressStateResultDto _addressStateResult;
        #endregion

        public async void Load()
        {
            try
            {
                AddressStateResult = await RestService.API.ApiAddressStateResultsAsync();
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
    }
}
