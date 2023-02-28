using NNDIP.Maui.Views.Dashboard;
using NNDIP.Maui.Views.Startup;
using NNDIP.Maui.Services;
using NNDIP.Maui.ViewModels.Dashboard;
using NNDIP.Maui.ViewModels.Startup;
//using Syncfusion.Maui.Core.Hosting;
using NNDIP.Maui.Controls;

namespace NNDIP.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
		builder
            .UseMauiApp<App>()
            //.ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<DashboardPage>();
        builder.Services.AddSingleton<LoadingPage>();

        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<DashboardPageViewModel>();
        builder.Services.AddSingleton<LoadingPageViewModel>();

        return builder.Build();
	}
}
