using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using NNDIP.Maui.Models;
using NNDIP.Maui.Services;

namespace NNDIP.Maui.ViewModels.Startup
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        #region Commands
        [RelayCommand]
        async void Login()
        {
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                var userDetails = new UserInfo();
                userDetails.Username = Username;

                ApiClient.TokenDto tokenDto = await RestService.API.LoginAsync(new ApiClient.LoginDto
                {
                    Username = Username,
                    Password = Password
                }); 

                if (Preferences.ContainsKey(nameof(App.UserDetails)))
                {
                    Preferences.Remove(nameof(App.UserDetails));
                }

                userDetails.Token = tokenDto.Token;
                string userDetailStr = JsonConvert.SerializeObject(userDetails);
                Preferences.Set(nameof(App.UserDetails), userDetailStr);
                App.UserDetails = userDetails;
                await AppConstant.AddFlyoutMenusDetails();
            }
        }
        #endregion
    }
}
