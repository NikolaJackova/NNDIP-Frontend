using NNDIP.Maui.ViewModels.Dashboard;

namespace NNDIP.Maui.Pages.Dashboard;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardPageViewModel dashboardPageViewModel)
	{
		InitializeComponent();
        BindingContext = dashboardPageViewModel;
    }
}