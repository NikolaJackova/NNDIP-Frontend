using NNDIP.Maui.ViewModels.Plan;

namespace NNDIP.Maui.Views.Plan;

public partial class LimitPlanPage : ContentPage
{
	public LimitPlanPage(LimitPlanPageViewModel limitPlanPageViewModel)
	{
		InitializeComponent();
		BindingContext = limitPlanPageViewModel;
    }

    override protected void OnAppearing()
    {
        base.OnAppearing();
        LimitPlanPageViewModel viewModel = BindingContext as LimitPlanPageViewModel;
        viewModel.Load();
    }
}