using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NNDIP.ApiClient;
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
                await ExceptionHandlingService.HandleException(ex);
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
        public async void AddManualPlan()
        {
            await Shell.Current.GoToAsync(nameof(AddUpdateManualPlanPage));
        }

        [RelayCommand]
        public async void EditManualPlan(ManualPlanDto manualPlanDto)
        {
            Dictionary<string, object> navParam = new Dictionary<string, object>
            {
                { nameof(ManualPlanDto), manualPlanDto }
            };
            await Shell.Current.GoToAsync(nameof(AddUpdateManualPlanPage), navParam);
        }

        [RelayCommand]
        public async void DeleteManualPlan(ManualPlanDto manualPlanDto)
        {
            try
            {
                await RestService.API.ApiManualPlanDeleteAsync(manualPlanDto.Id);
            }
            catch (ApiClientException ex)
            {
                await ExceptionHandlingService.HandleException(ex);
                return;
            }
            Load();
        }

        [RelayCommand]
        public async void Toggled(ManualPlanDto manualPlanDto)
        {
            if (manualPlanDto is not null)
            {
                try
                {
                    await RestService.API.ApiManualPlanPutAsync(manualPlanDto.Id, new UpdateManualPlanDto()
                    {
                        Id = manualPlanDto.Id,
                        IdNavigation = new UpdatePlanDto()
                        {
                            Id = manualPlanDto.Id,
                            Enabled = manualPlanDto.IdNavigation.Enabled,
                            EventId = manualPlanDto.IdNavigation.EventId,
                            Name = manualPlanDto.IdNavigation.Name,
                            Priority = manualPlanDto.IdNavigation.Priority,
                            PlanType = manualPlanDto.IdNavigation.PlanType
                        }
                    });
                }
                catch (ApiClientException ex)
                {
                    await ExceptionHandlingService.HandleException(ex);
                }
            }
        }
        #endregion
    }
}
