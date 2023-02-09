using NNDIP.Maui.Controls;
using NNDIP.Maui.Views.Dashboard;
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
                Title = "Dashboard Page",
                Route = nameof(DashboardPage),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                            {
                                new ShellContent
                                {
                                    Icon = Icons.Dashboard,
                                    Title = "Dashboard",
                                    ContentTemplate = new DataTemplate(typeof(DashboardPage)),
                                },
                                new ShellContent
                                {
                                    Icon = Icons.Dashboard,
                                    Title = "Dashboard 1",
                                    ContentTemplate = new DataTemplate(typeof(DashboardPage)),
                                },
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
