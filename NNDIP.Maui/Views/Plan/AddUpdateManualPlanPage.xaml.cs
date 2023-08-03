using NNDIP.Maui.ViewModels.Plan;

namespace NNDIP.Maui.Views.Plan;

public partial class AddUpdateManualPlanPage : ContentPage
{
	public AddUpdateManualPlanPage(AddUpdateManualPlanPageViewModel addUpdateManualPlanPageViewModel)
	{
		InitializeComponent();
		BindingContext = addUpdateManualPlanPageViewModel;
    }

    override protected void OnAppearing()
    {
        base.OnAppearing();
        AddUpdateManualPlanPageViewModel viewModel = BindingContext as AddUpdateManualPlanPageViewModel;
        viewModel.Load();
    }
}