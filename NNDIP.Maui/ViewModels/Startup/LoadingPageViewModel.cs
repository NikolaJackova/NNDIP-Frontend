using NNDIP.Maui.Models;
using NNDIP.Maui.Services;
using NNDIP.Maui.Views.Startup;

namespace NNDIP.Maui.ViewModels.Startup
{
    public class LoadingPageViewModel
    {
        public LoadingPageViewModel()
        {
            CheckUserLoginDetails();
        }
        private async void CheckUserLoginDetails()
        {
            string token = await AuthenticationService.GetJwtToken();
            if (token is null)
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    Shell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                    });
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
            }
            else
            {
                await AppConstant.AddFlyoutMenusDetails();
            }
        }

    }
}
