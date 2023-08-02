using NNDIP.ApiClient;
using NNDIP.Maui.Views.Startup;

namespace NNDIP.Maui.Services
{
    public static class ExceptionHandlingService
    {
        public static async Task HandleException(Exception ex)
        {
            if (ex is ApiClientException)
            {
                if (((ApiClientException)ex).StatusCode == 401)
                {
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                    return;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ((ApiClientException)ex).Response, "Ok");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}
