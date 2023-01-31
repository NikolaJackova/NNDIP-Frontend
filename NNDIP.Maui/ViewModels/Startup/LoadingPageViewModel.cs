using Newtonsoft.Json;
using NNDIP.Maui.Models;
using NNDIP.Maui.Pages.Startup;

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
            string userDetailsStr = Preferences.Get(nameof(App.UserDetails), "");

            if (string.IsNullOrWhiteSpace(userDetailsStr))
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
                var userInfo = JsonConvert.DeserializeObject<UserInfo>(userDetailsStr);
                App.UserDetails = userInfo;
                await AppConstant.AddFlyoutMenusDetails();
            }
        }

    }
}
