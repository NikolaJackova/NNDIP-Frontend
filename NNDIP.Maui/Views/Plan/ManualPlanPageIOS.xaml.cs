using NNDIP.Maui.ViewModels.Plan;

namespace NNDIP.Maui.Views.Plan;

public partial class ManualPlanPageIOS : ContentPage
{
	public ManualPlanPageIOS(ManualPlanPageViewModel manualPlanPageViewModel)
	{
		InitializeComponent();
		BindingContext = manualPlanPageViewModel;
	}

    override protected void OnAppearing()
    {
        base.OnAppearing();
        ManualPlanPageViewModel viewModel = BindingContext as ManualPlanPageViewModel;
        viewModel.IsRefreshing = true;
    }
}