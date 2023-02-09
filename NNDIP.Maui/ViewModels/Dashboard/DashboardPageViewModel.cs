using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using NNDIP.ApiClient;
using NNDIP.Maui.Controls;
using NNDIP.Maui.Models;
using NNDIP.Maui.Services;

namespace NNDIP.Maui.ViewModels.Dashboard;

public partial class DashboardPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private AddressStateResultDto _addressStateResult;
    public DashboardPageViewModel()
	{
        Shell.Current.FlyoutHeader = new FlyoutHeaderControl();
    }

    #region Commands
    [RelayCommand]
    async void Load()
    {
        try
        {
            AddressStateResult = await RestService.API.ApiAddressStateResultsAsync();
        }
        catch (ApiClientException ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            return;
        }
    }
    #endregion
}