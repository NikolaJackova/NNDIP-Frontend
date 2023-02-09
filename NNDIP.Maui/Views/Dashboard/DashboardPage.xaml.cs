using NNDIP.Maui.ViewModels.Dashboard;

namespace NNDIP.Maui.Views.Dashboard;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardPageViewModel dashboardPageViewModel)
	{
		InitializeComponent();
        BindingContext = dashboardPageViewModel;
    }
}