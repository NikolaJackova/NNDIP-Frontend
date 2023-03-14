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
        AppCenter.Start("ios=c0569519-d7d8-41ba-b887-770f6a36062e;android=a7a21ff8-a7f0-4e21-8be2-f848e7265d17", typeof(Analytics), typeof(Crashes), typeof(Distribute));
        AppCenter.LogLevel = LogLevel.Verbose;
        AuthenticationService.Init();
    }
}
