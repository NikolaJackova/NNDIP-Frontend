﻿using NNDIP.Maui.Views.Dashboard;
using NNDIP.Maui.Views.Startup;
using NNDIP.Maui.Services;
using NNDIP.Maui.ViewModels.Dashboard;
using NNDIP.Maui.ViewModels.Startup;
using Syncfusion.Maui.Core.Hosting;
using NNDIP.Maui.Controls;
using NNDIP.Maui.Views.Plan;
using NNDIP.Maui.ViewModels.Plan;
using CommunityToolkit.Maui;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace NNDIP.Maui;

public static class MauiProgram
{
    private const string Namespace = "NNDIP.Maui";
    private const string File = "secrets.json";
	public static MauiApp CreateMauiApp()
	{
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{Namespace}.{File}");
       
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureSyncfusionCore()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
            .Configuration.AddJsonStream(stream);

        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<LoadingPage>();
        builder.Services.AddSingleton<DashboardPage>();
        builder.Services.AddSingleton<LimitPlanPage>();
        builder.Services.AddSingleton<TimePlanPage>();
        builder.Services.AddSingleton<AddUpdateTimePlanPage>();
        builder.Services.AddSingleton<ManualPlanPage>();
        builder.Services.AddSingleton<AddUpdateManualPlanPage>();

        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<LoadingPageViewModel>();
        builder.Services.AddSingleton<DashboardPageViewModel>();
        builder.Services.AddSingleton<LimitPlanPageViewModel>();
        builder.Services.AddSingleton<TimePlanPageViewModel>();
        builder.Services.AddSingleton<AddUpdateTimePlanPageViewModel>();
        builder.Services.AddSingleton<ManualPlanPageViewModel>();
        builder.Services.AddSingleton<AddUpdateManualPlanPageViewModel>();

        return builder.Build();
	}
}
