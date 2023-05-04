using NNDIP.Maui.Controls;
using NNDIP.Maui.Resources.Languages;
using NNDIP.Maui.Views.Dashboard;
using NNDIP.Maui.Views.Plan;

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
                                    Icon = Icons.LimitPlan,
                                    Title = AppResources.limitPlanPage_PageTitle,
                                    ContentTemplate = new DataTemplate(typeof(LimitPlanPage)),
                                },
                                new ShellContent
                                {
                                    Icon = Icons.TimePlan,
                                    Title = AppResources.timePlanPage_PageTitle,
#if IOS
                                    ContentTemplate = new DataTemplate(typeof(TimePlanPageIOS)),
#else
                                    ContentTemplate = new DataTemplate(typeof(TimePlanPage)),
#endif
                                },
                                new ShellContent
                                {
                                    Icon = Icons.ManualPlan,
                                    Title = AppResources.manualPlanPage_PageTitle,
#if IOS
                                    ContentTemplate = new DataTemplate(typeof(ManualPlanPageIOS)),
#else
                                    ContentTemplate = new DataTemplate(typeof(ManualPlanPage)),
#endif
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
