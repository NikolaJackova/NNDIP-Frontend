using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NNDIP.ApiClient;
using NNDIP.Maui.Enums;
using NNDIP.Maui.Services;
using NNDIP.Maui.Views.Startup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NNDIP.Maui.ViewModels.Plan
{
    public partial class LimitPlanPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private LimitPlanSettings _limitSettings;

        [ObservableProperty]
        private SimpleYearPeriodDto _summerPeriod;

        [ObservableProperty]
        private SimpleYearPeriodDto _winterPeriod;

        [ObservableProperty]
        private ObservableCollection<SimpleEventDto> _events;

        [ObservableProperty]
        private SimpleEventDto _temperatureLowEvent;

        [ObservableProperty]
        private SimpleEventDto _temperatureHighEvent;

        [ObservableProperty]
        private SimpleEventDto _co2Event;

        [ObservableProperty]
        private bool _isRefreshing;

        public async void Load()
        {
            try
            {
                ICollection<SimpleYearPeriodDto> simpleYearPeriodDtos = await RestService.API.ApiYearPeriodGetAsync();
                foreach (var item in simpleYearPeriodDtos)
                {
                    if (item.Name == EnumExtender.GetEnumDescription(YearPeriodType.WINTER))
                    {
                        WinterPeriod = item;
                    }
                    else if (item.Name == EnumExtender.GetEnumDescription(YearPeriodType.SUMMER))
                    {
                        SummerPeriod = item;
                    }
                }
                LimitSettings = await RestService.API.ApiLimitPlanSettingsGetAsync();
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
            TemperatureLowEvent = Events.FirstOrDefault(item => item.Id == LimitSettings.TemperatureLow.EventId);
            TemperatureHighEvent = Events.FirstOrDefault(item => item.Id == LimitSettings.TemperatureHigh.EventId);
            Co2Event = Events.FirstOrDefault(item => item.Id == LimitSettings.Co2.EventId);
        }

        #region Commands
        [RelayCommand]
        void Refresh()
        {
            Load();
            IsRefreshing = false;
        }

        [RelayCommand]
        async void Save()
        {
            LimitSettings.TemperatureHigh.EventId = TemperatureHighEvent.Id;
            LimitSettings.TemperatureLow.EventId = TemperatureLowEvent.Id;
            LimitSettings.Co2.EventId = Co2Event.Id;
            if (WinterPeriod.Active == 1)
            {
                LimitSettings.YearPeriodDto = WinterPeriod;
            }
            else
            {
                LimitSettings.YearPeriodDto = SummerPeriod;
            }
            await RestService.API.ApiLimitPlanSettingsPutAsync(LimitSettings);
            Load();
        }
        #endregion
    }
}
