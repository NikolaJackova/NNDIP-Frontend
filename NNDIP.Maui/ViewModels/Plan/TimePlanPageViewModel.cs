using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NNDIP.ApiClient;
using NNDIP.Maui.Resources.Languages;
using NNDIP.Maui.Services;
using NNDIP.Maui.Views.Plan;
using NNDIP.Maui.Views.Startup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNDIP.Maui.ViewModels.Plan
{
    public partial class TimePlanPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private ObservableCollection<TimePlanDto> _timePlans;

        public async void Load()
        {
            try
            {
                TimePlans = new ObservableCollection<TimePlanDto>(await RestService.API.ApiTimePlanGetAsync());
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

        [RelayCommand]
        void Save()
        {
            Load();
        }

        [RelayCommand]
        public async void AddTimePlan()
        {
            await Shell.Current.GoToAsync(nameof(AddUpdateTimePlanPage));
        }

        [RelayCommand]
        public async void EditTimePlan(TimePlanDto timePlanDto)
        {
            Dictionary<string, object> navParam = new Dictionary<string, object>
            {
                { nameof(TimePlanDto), timePlanDto }
            };
            await Shell.Current.GoToAsync(nameof(AddUpdateTimePlanPage), navParam);
        }

        [RelayCommand]
        public async void DeleteTimePlan(TimePlanDto timePlanDto)
        {
            await RestService.API.ApiTimePlanDeleteAsync(timePlanDto.Id);
            Load();
        }
        #endregion
    }
}
