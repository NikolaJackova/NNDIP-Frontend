using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NNDIP.ApiClient;
using NNDIP.Maui.Services;
using NNDIP.Maui.Views.Startup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Provider.CalendarContract;

namespace NNDIP.Maui.ViewModels.Plan
{
    public partial class ManualPlanPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private ObservableCollection<ManualPlanDto> _manualPlans;
        public async void Load()
        {
            try
            {
                ManualPlans = new ObservableCollection<ManualPlanDto>(await RestService.API.ApiManualPlanGetAsync());
            }
            catch (ApiClientException ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                if (ex.StatusCode == 401)
                {
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
                return;
            }
        }

        #region Commands
        [RelayCommand]
        void Refresh()
        {
            Load();
            IsRefreshing = false;
        }
        #endregion
    }
}
