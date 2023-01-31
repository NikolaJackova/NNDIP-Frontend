using NNDIP.Maui.Controls;

namespace NNDIP.Maui.ViewModels.Dashboard;

public class DashboardPageViewModel : BaseViewModel
{
	public DashboardPageViewModel()
	{
        AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
    }
}