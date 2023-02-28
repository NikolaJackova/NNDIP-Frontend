using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NNDIP.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NNDIP.Maui.ViewModels.Plan
{
    public partial class LimitPlanPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private bool _isRefreshing;

        #region Commands
        [RelayCommand]
        void Refresh()
        {
            IsRefreshing = false;
        }
        #endregion
    }
}
