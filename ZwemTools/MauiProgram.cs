// <copyright file="MauiProgram.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Radzen;
using Serilog;
using Serilog.Events;
using Velopack;
using ZwemTools.Abstractions;
using ZwemTools.Data.Lenex;
using ZwemTools.Data.Sql;
using ZwemTools.Data.TeamManager;

namespace ZwemTools;

/// <summary>
/// Application entrypoint.
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// Creates and initialized the MAUI application.
    /// </summary>
    /// <returns>The application.</returns>
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();

        SetupSerilog();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif
        builder.Services.AddLocalization();

        builder.Services.AddSingleton<LenexParser>();
        builder.Services.AddSingleton<BoomsmaService>();

        if (OperatingSystem.IsWindows())
        {
            builder.Services.AddScoped<ITeamManagerDatabase, TeamManagerDatabase>();
            builder.Services.AddSingleton<RelaysService>();
        }

        builder.Services.AddRadzenServices();

        builder.Services.AddDbContext<DatabaseContext>();

        builder.Services.AddScoped<IPreferenceService, PreferenceService>();

        builder.Logging.AddDebug();
        builder.Logging.AddEventLog();
        builder.Logging.AddSerilog(dispose: true);

        MauiApp app = builder.Build();

        VelopackApp.Build().Run(app.Services.GetRequiredService<ILogger<VelopackApp>>());

        RunStartupTasks(app);

        return app;
    }

    private static void SetupSerilog()
    {
        TimeSpan flushInterval = new(0, 0, 1);
        string file = Path.Combine(FileSystem.AppDataDirectory, "logs", "ZwemTools.log");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.File(file, flushToDiskInterval: flushInterval, encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    private static void RunStartupTasks(MauiApp app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        DatabaseContext db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        db.Database.Migrate();
    }

    private static IServiceCollection AddRadzenServices(this IServiceCollection services)
    {
        services.AddScoped<DialogService>();
        services.AddScoped<NotificationService>();
        services.AddScoped<ContextMenuService>();
        return services;
    }
}
