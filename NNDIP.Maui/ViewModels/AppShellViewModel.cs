using CommunityToolkit.Mvvm.Input;
using NNDIP.Maui.Views.Startup;
using NNDIP.Maui.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNDIP.Maui.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        #region Commands
        [RelayCommand]
        async void SignOut()
        {
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                Preferences.Remove(nameof(App.UserDetails));
                RestService.ClearAuthorization();
            }
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
        #endregion
    }
}
