using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NNDIP.ApiClient;
using NNDIP.Maui.Models;
using NNDIP.Maui.Services;

namespace NNDIP.Maui.ViewModels.Startup
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        #region Properites
        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;
        #endregion

        #region Commands
        [RelayCommand]
        async void Login()
        {
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                TokenDto tokenDto;
                try
                {
                    tokenDto = await RestService.API.ApiLoginAsync(new LoginDto
                    {
                        Username = Username,
                        Password = Password
                    });
                }
                catch (Exception ex)
                {
                    await ExceptionHandlingService.HandleException(ex);
                    return;
                }

                AuthenticationService.RemoveJwtToken();
                AuthenticationService.SetJwtToken(tokenDto.Token);
                RestService.SetAuthorization(tokenDto.Token);
                await AppConstant.AddFlyoutMenusDetails();
            }
        }
        #endregion
    }
}
