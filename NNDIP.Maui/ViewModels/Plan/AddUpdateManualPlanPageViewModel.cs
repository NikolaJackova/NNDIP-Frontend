using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NNDIP.ApiClient;
using NNDIP.Maui.Services;
using System.Collections.ObjectModel;

namespace NNDIP.Maui.ViewModels.Plan
{
    [QueryProperty("ManualPlan", nameof(ManualPlanDto))]
    public partial class AddUpdateManualPlanPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ManualPlanDto _manualPlan;
        [ObservableProperty]
        private ObservableCollection<SimpleEventDto> _events;

        [ObservableProperty]
        private SimpleEventDto _manualPlanEvent;

        public async void Load()
        {
            ManualPlan ??= new ManualPlanDto()
                {
                    IdNavigation = new SimplePlanDto()
                };
            try
            {
                Events = new ObservableCollection<SimpleEventDto>(await RestService.API.ApiEventGetAsync());
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleException(ex);
            }
            SetSelectedEvents();
        }
        private void SetSelectedEvents()
        {
            if (ManualPlan.Id > 0)
            {
                ManualPlanEvent = Events.FirstOrDefault(item => item.Id == ManualPlan.IdNavigation.EventId);
            }
            else
            {
                ManualPlanEvent = Events.FirstOrDefault();
            }
        }

        #region Commands
        [RelayCommand]
        public async void AddUpdateManualPlan()
        {
            if (ManualPlan.Id > 0)
            {
                try
                {
                    await RestService.API.ApiManualPlanPutAsync(ManualPlan.Id, new UpdateManualPlanDto()
                    {
                        Id = ManualPlan.Id,
                        IdNavigation = new UpdatePlanDto()
                        {
                            Id = ManualPlan.Id,
                            Enabled = ManualPlan.IdNavigation.Enabled,
                            Name = ManualPlan.IdNavigation.Name,
                            EventId = ManualPlanEvent.Id,
                            PlanType = ManualPlan.IdNavigation.PlanType,
                            Priority = ManualPlan.IdNavigation.Priority
                        }
                    });
                }
                catch (Exception ex)
                {
                    await ExceptionHandlingService.HandleException(ex);
                    return;
                }
            }
            else
            {
                try
                {
                    await RestService.API.ApiManualPlanPostAsync(new AddManualPlanDto()
                    {
                        IdNavigation = new AddPlanDto()
                        {
                            Enabled = 1,
                            EventId = ManualPlanEvent.Id,
                            Name = ManualPlan.IdNavigation.Name
                        }
                    });
                }
                catch (Exception ex)
                {
                    await ExceptionHandlingService.HandleException(ex);
                    return;
                }
            }
            await Shell.Current.GoToAsync("..");
        }
        #endregion
    }
}
