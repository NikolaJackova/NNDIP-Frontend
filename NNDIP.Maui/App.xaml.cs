using Microsoft.Extensions.Configuration;
using NNDIP.Maui.Services;

namespace NNDIP.Maui;

public partial class App : Application
{
    public App(IConfiguration configuration)
	{
        InitializeComponent();
        MainPage = new AppShell();
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(configuration["Syncfusion"]);
        AuthenticationService.Init();
    }
}
