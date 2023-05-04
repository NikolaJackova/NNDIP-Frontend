using CommunityToolkit.Mvvm.Input;
using NNDIP.Maui.Views.Startup;
using NNDIP.Maui.Services;

namespace NNDIP.Maui.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        #region Commands
        [RelayCommand]
        async void SignOut()
        {
            AuthenticationService.RemoveJwtToken();
            RestService.ClearAuthorization();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
        #endregion
    }
}
