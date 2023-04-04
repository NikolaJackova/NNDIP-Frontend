using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using NNDIP.Maui.Models;
using NNDIP.Maui.Services;

namespace NNDIP.Maui;

public partial class App : Application
{
    public App()
	{
		InitializeComponent();
        MainPage = new AppShell();
        AppCenter.Start("ios=4a0546f6-e03d-4cfe-ae83-07dd9610df86;android=24354f58-b2aa-4205-8c4c-fefd3dc0eb3f", typeof(Analytics), typeof(Crashes));
        AppCenter.LogLevel = LogLevel.Verbose;
        //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense();
        AuthenticationService.Init();
    }
}
