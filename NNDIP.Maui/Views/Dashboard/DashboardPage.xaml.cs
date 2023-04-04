using NNDIP.Maui.ViewModels.Dashboard;

namespace NNDIP.Maui.Views.Dashboard;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardPageViewModel dashboardPageViewModel)
	{
		InitializeComponent();
        BindingContext = dashboardPageViewModel;
    }
    override protected void OnAppearing()
    {
        base.OnAppearing();
        DashboardPageViewModel viewModel = BindingContext as DashboardPageViewModel;
        viewModel.SetDefault();
        viewModel.IsRefreshing = true;
    }
}