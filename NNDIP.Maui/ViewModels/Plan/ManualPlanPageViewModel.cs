using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NNDIP.ApiClient;
using NNDIP.Maui.Services;
using NNDIP.Maui.Views.Plan;
using System.Collections.ObjectModel;

namespace NNDIP.Maui.ViewModels.Plan
{
    public partial class ManualPlanPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private ObservableCollection<ManualPlanDto> _manualPlans;

        public async Task Load()
        {
            try
            {
                ManualPlans = new ObservableCollection<ManualPlanDto>(await RestService.API.ApiManualPlanGetAsync());
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleException(ex);
            }
        }

        #region Commands
        [RelayCommand]
        async void Refresh()
        {
            await Load();
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
            catch (Exception ex)
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
                catch (Exception ex)
                {
                    await ExceptionHandlingService.HandleException(ex);
                }
            }
        }
        #endregion
    }
}
