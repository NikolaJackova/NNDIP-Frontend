using NNDIP.Maui.ViewModels.Dashboard;

namespace NNDIP.Maui.Views.Dashboard;

public partial class DashboardTest : ContentPage
{
	public DashboardTest(DashboardTestViewModel dashboardTestViewModel)
	{
		InitializeComponent();
        BindingContext = dashboardTestViewModel;
    }
}