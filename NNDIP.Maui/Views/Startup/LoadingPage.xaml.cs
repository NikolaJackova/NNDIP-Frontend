using NNDIP.Maui.ViewModels.Startup;

namespace NNDIP.Maui.Views.Startup;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingPageViewModel loadingPageViewModel)
	{
		InitializeComponent();
		BindingContext = loadingPageViewModel;
	}
}