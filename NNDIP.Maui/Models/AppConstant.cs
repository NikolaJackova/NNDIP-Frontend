using NNDIP.Maui.Controls;
using NNDIP.Maui.Resources.Languages;
using NNDIP.Maui.Views.Dashboard;
using NNDIP.Maui.Views.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNDIP.Maui.Models
{
    public class AppConstant
    {

        public async static Task AddFlyoutMenusDetails()
        {
            Shell.Current.FlyoutHeader = new FlyoutHeaderControl();

            var dashboardInfo = Shell.Current.Items.Where(f => f.Route == nameof(DashboardPage)).FirstOrDefault();
            if (dashboardInfo != null) Shell.Current.Items.Remove(dashboardInfo);
            var flyoutItem = new FlyoutItem()
            {
                Title = AppResources.dashboardPage_PageTitle,
                Route = nameof(DashboardPage),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                            {
                                new ShellContent
                                {
                                    Icon = Icons.Dashboard,
                                    Title = AppResources.dashboardPage_PageTitle,
                                    ContentTemplate = new DataTemplate(typeof(DashboardPage)),
                                },
                                new ShellContent
                                {
                                    Icon = Icons.Dashboard,
                                    Title = AppResources.limitPlanPage_PageTitle,
                                    ContentTemplate = new DataTemplate(typeof(LimitPlanPage)),
                                },
                                new ShellContent
                                {
                                    Icon = Icons.Dashboard,
                                    Title = AppResources.timePlanPage_PageTitle,
                                    ContentTemplate = new DataTemplate(typeof(TimePlanPage)),
                                },
                                new ShellContent
                                {
                                    Icon = Icons.Dashboard,
                                    Title = AppResources.manualPlanPage_PageTitle,
                                    ContentTemplate = new DataTemplate(typeof(ManualPlanPage)),
                                }
                            }
            };
            if (!Shell.Current.Items.Contains(flyoutItem))
            {
                Shell.Current.Items.Add(flyoutItem);
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    Shell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
                    });
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
                }
            }
        }
    }
}
