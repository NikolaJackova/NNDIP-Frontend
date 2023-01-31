namespace NNDIP.Maui.Controls;

public partial class FlyoutHeaderControl : StackLayout
{
    public FlyoutHeaderControl()
    {
        InitializeComponent();

        if (App.UserDetails != null)
        {
            labelUsername.Text = App.UserDetails.Username;
        }
    }
}