using NNDIP.Maui.ViewModels;
using NNDIP.Maui.Views.Plan;

namespace NNDIP.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        BindingContext = new AppShellViewModel();
        Routing.RegisterRoute(nameof(AddUpdateTimePlanPage), typeof(AddUpdateTimePlanPage));
        Routing.RegisterRoute(nameof(AddUpdateManualPlanPage), typeof(AddUpdateManualPlanPage));
    }
}
