using NNDIP.Maui.ViewModels.Startup;

namespace NNDIP.Maui.Views.Startup;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel loginPageViewModel)
	{
		InitializeComponent();
		BindingContext = loginPageViewModel;
	}
}