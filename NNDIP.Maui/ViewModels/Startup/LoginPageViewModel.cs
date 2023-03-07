using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
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
                var userDetails = new UserInfo
                {
                    Username = Username
                };
                TokenDto tokenDto;
                try
                {
                    tokenDto = await RestService.API.ApiLoginAsync(new LoginDto
                    {
                        Username = Username,
                        Password = Password
                    });
                }
                catch (ApiClientException ex)
                {
                    await ExceptionHandlingService.HandleException(ex);
                    return;
                }

                SecureStorageService.Remove();
                SecureStorageService.Set(tokenDto.Token);
                RestService.SetAuthorization(tokenDto.Token);
                App.UserDetails = userDetails;
                await AppConstant.AddFlyoutMenusDetails();
            }
        }
        #endregion
    }
}
