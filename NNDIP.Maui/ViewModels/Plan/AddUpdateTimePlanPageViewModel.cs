using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NNDIP.ApiClient;
using NNDIP.Maui.Resources.Languages;
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
    [QueryProperty("TimePlan", nameof(TimePlanDto))]
    public partial class AddUpdateTimePlanPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public TimePlanDto _timePlan = new TimePlanDto()
        {

            IdNavigation = new SimplePlanDto()
        };

        [ObservableProperty]
        private ObservableCollection<SimpleEventDto> _events;

        [ObservableProperty]
        private SimpleEventDto _timePlanEvent;

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
            if (TimePlan is not null)
            {
                TimePlanEvent = Events.FirstOrDefault(item => item.Id == TimePlan.IdNavigation.EventId);
            }
        }

        #region Commands
        [RelayCommand]
        public async void AddUpdateTimePlan()
        {
            if (TimePlan.Id > 0)
            {
                await RestService.API.ApiTimePlanPutAsync(TimePlan.Id, null);
            }
            else
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
        }
        #endregion
    }
}
