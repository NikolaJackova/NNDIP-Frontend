using NNDIP.Maui.ViewModels.Startup;

namespace NNDIP.Maui.Pages.Startup;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingPageViewModel loadingPageViewModel)
	{
		InitializeComponent();
		BindingContext = loadingPageViewModel;
	}
}