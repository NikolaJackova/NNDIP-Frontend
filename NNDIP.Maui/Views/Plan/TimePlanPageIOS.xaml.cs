using NNDIP.Maui.ViewModels.Plan;

namespace NNDIP.Maui.Views.Plan;

public partial class TimePlanPageIOS : ContentPage
{
	public TimePlanPageIOS(TimePlanPageViewModel timePlanPageViewModel)
	{
		InitializeComponent();
		BindingContext = timePlanPageViewModel;
	}

    override protected void OnAppearing()
    {
        base.OnAppearing();
        TimePlanPageViewModel viewModel = BindingContext as TimePlanPageViewModel;
        viewModel.IsRefreshing = true;
    }
}