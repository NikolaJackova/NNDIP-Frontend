using NNDIP.Maui.Services;

namespace NNDIP.Maui.Controls;

public partial class FlyoutHeaderControl : StackLayout
{
    public FlyoutHeaderControl()
    {
        InitializeComponent();
        labelUsername.Text = AuthenticationService.GetUsername();
    }
}