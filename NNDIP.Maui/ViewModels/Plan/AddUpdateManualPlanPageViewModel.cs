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

namespace NNDIP.Maui.ViewModels.Plan
{
    [QueryProperty("ManualPlan", nameof(ManualPlanDto))]
    public partial class AddUpdateManualPlanPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ManualPlanDto _manualPlan = new ManualPlanDto()
        {
            IdNavigation = new SimplePlanDto()
        };
        [ObservableProperty]
        private ObservableCollection<SimpleEventDto> _events;

        [ObservableProperty]
        private SimpleEventDto _manualPlanEvent;

        public async void Load()
        {
            try
            {
                Events = new ObservableCollection<SimpleEventDto>(await RestService.API.ApiEventGetAsync());
                SetSelectedEvents();
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
        private void SetSelectedEvents()
        {
            if (ManualPlan is not null)
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
                await RestService.API.ApiManualPlanPutAsync(ManualPlan.Id, new UpdateManualPlanDto()
                {
                    Id = ManualPlan.Id,
                    IdNavigation = new UpdatePlanDto()
                    {
                        Id = ManualPlan.Id,
                        Enabled = ManualPlan.IdNavigation.Enabled,
                        Name = ManualPlan.IdNavigation.Name,
                        EventId = ManualPlan.IdNavigation.EventId,
                        PlanType = ManualPlan.IdNavigation.PlanType,
                        Priority = ManualPlan.IdNavigation.Priority
                    }
                });
            }
            else
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
            await Shell.Current.GoToAsync("..");
        }
        #endregion
    }
}
