using NNDIP.Maui.ViewModels;

namespace NNDIP.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        BindingContext = new AppShellViewModel();
    }
}
