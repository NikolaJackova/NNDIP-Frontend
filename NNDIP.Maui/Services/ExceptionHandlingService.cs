using NNDIP.ApiClient;
using NNDIP.Maui.Views.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNDIP.Maui.Services
{
    public static class ExceptionHandlingService
    {
        public static async Task HandleException(ApiClientException ex)
        {

            if (ex.StatusCode == 401)
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}
