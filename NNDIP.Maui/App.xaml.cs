using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Configuration;
using NNDIP.Maui.Services;

namespace NNDIP.Maui;

public partial class App : Application
{
    public App(IConfiguration configuration)
	{
        InitializeComponent();
        MainPage = new AppShell();
        /*try
        {
            AppCenter.Start($"ios={configuration["iOS:Key"]};android={configuration["Android:Key"]}", typeof(Analytics), typeof(Crashes));
        }
        catch (Exception ex)
        {
            string a = ex.Message;
            Console.WriteLine(a);
        }*/
        //AppCenter.LogLevel = LogLevel.Verbose;
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(configuration["Syncfusion"]);
        AuthenticationService.Init();
    }
}
