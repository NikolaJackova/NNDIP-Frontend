using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NNDIP.ApiClient;
using NNDIP.Maui.Services;
using System.Collections.ObjectModel;

namespace NNDIP.Maui.ViewModels.Plan
{
    [QueryProperty("TimePlan", nameof(TimePlanDto))]
    public partial class AddUpdateTimePlanPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public TimePlanDto _timePlan;

        [ObservableProperty]
        private ObservableCollection<SimpleEventDto> _events;

        [ObservableProperty]
        private SimpleEventDto _timePlanEvent;

        public async void Load()
        {
            TimePlan ??= new TimePlanDto()
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
            if (TimePlan.Id > 0)
            {
                TimePlanEvent = Events.FirstOrDefault(item => item.Id == TimePlan.IdNavigation.EventId);
            }
            else
            {
                TimePlanEvent = Events.FirstOrDefault();
            }
        }

        #region Commands
        [RelayCommand]
        public async void AddUpdateTimePlan()
        {
            if (TimePlan.Id > 0)
            {
                try
                {
                    await RestService.API.ApiTimePlanPutAsync(TimePlan.Id, new UpdateTimePlanDto()
                    {
                        Id = TimePlan.Id,
                        FromTime = TimePlan.FromTime,
                        ToTime = TimePlan.ToTime,
                        IdNavigation = new UpdatePlanDto()
                        {
                            Id = TimePlan.Id,
                            Enabled = TimePlan.IdNavigation.Enabled,
                            Name = TimePlan.IdNavigation.Name,
                            EventId = TimePlanEvent.Id,
                            PlanType = TimePlan.IdNavigation.PlanType,
                            Priority = TimePlan.IdNavigation.Priority
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
                    await RestService.API.ApiTimePlanPostAsync(new AddTimePlanDto()
                    {
                        FromTime = TimePlan.FromTime,
                        ToTime = TimePlan.ToTime,
                        IdNavigation = new AddPlanDto()
                        {
                            Enabled = 1,
                            EventId = TimePlanEvent.Id,
                            Name = TimePlan.IdNavigation.Name
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
