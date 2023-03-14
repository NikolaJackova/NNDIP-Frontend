using NNDIP.Maui.ViewModels.Plan;

namespace NNDIP.Maui.Views.Plan;

public partial class AddUpdateTimePlanPage : ContentPage
{
	public AddUpdateTimePlanPage(AddUpdateTimePlanPageViewModel addUpdateTimePlanPageViewModel)
	{
		InitializeComponent();
		BindingContext = addUpdateTimePlanPageViewModel;
    }

    override protected void OnAppearing()
    {
        base.OnAppearing();
        AddUpdateTimePlanPageViewModel viewModel = BindingContext as AddUpdateTimePlanPageViewModel;
        viewModel.Load();
    }
}