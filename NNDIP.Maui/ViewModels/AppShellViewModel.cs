using CommunityToolkit.Mvvm.Input;
using NNDIP.Maui.Pages.Startup;
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
            }
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
        #endregion
    }
}
