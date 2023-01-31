using NNDIP.Maui.ViewModels.Startup;

namespace NNDIP.Maui.Pages.Startup;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel loginPageViewModel)
	{
		InitializeComponent();
		BindingContext = loginPageViewModel;
	}
}