using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls.PlatformConfiguration;
using NNDIP.Maui.Models;
using NNDIP.Maui.Services;

namespace NNDIP.Maui;

public partial class App : Application
{
    public App(IConfiguration configuration)
	{
        InitializeComponent();
        MainPage = new AppShell();
        AppCenter.Start($"ios={configuration["iOS:Key"]};android={configuration["Android:Key"]}", typeof(Analytics), typeof(Crashes));
        AppCenter.LogLevel = LogLevel.Verbose;
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(configuration["Syncfusion"]);
        AuthenticationService.Init();
    }
}
