﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NNDIP.ApiClient;
using NNDIP.Maui.Enums;
using NNDIP.Maui.Services;
using System.Collections.ObjectModel;

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

        public async Task Load()
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
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleException(ex);
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
        public async void Refresh()
        {
            await Load();
            IsRefreshing = false;
        }

        [RelayCommand]
        public async void Save()
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
            try
            {
                await RestService.API.ApiLimitPlanSettingsPutAsync(LimitSettings);
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleException(ex);
                return;
            }
            IsRefreshing = true;
        }

        [RelayCommand]
        public async void CheckedChanged()
        {
            UpdateYearPeriodDto updateYearPeriodDto = new UpdateYearPeriodDto()
            {
                Active = 1
            };
            if (SummerPeriod.Active == 1)
            {
                updateYearPeriodDto.Id = SummerPeriod.Id;
            } else
            {
                updateYearPeriodDto.Id = WinterPeriod.Id;
            }
            try
            {
                await RestService.API.ApiYearPeriodPutAsync(updateYearPeriodDto.Id, updateYearPeriodDto);
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleException(ex);
                return;
            }
            Load();
        }
        #endregion
    }
}
