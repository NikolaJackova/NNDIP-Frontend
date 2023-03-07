﻿using CommunityToolkit.Mvvm.ComponentModel;
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
            try
            {
                await RestService.API.ApiTimePlanDeleteAsync(timePlanDto.Id);
            }
            catch (ApiClientException ex)
            {
                await ExceptionHandlingService.HandleException(ex);
                return;
            }
            Load();
        }

        [RelayCommand]
        public async void Toggled(TimePlanDto timePlanDto)
        {
            if (timePlanDto is not null)
            {
                try
                {
                    await RestService.API.ApiTimePlanPutAsync(timePlanDto.Id, new UpdateTimePlanDto()
                    {
                        Id = timePlanDto.Id,
                        FromTime = timePlanDto.FromTime,
                        ToTime = timePlanDto.ToTime,
                        IdNavigation = new UpdatePlanDto()
                        {
                            Id = timePlanDto.Id,
                            Enabled = timePlanDto.IdNavigation.Enabled,
                            EventId = timePlanDto.IdNavigation.EventId,
                            Name = timePlanDto.IdNavigation.Name,
                            Priority = timePlanDto.IdNavigation.Priority,
                            PlanType = timePlanDto.IdNavigation.PlanType
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