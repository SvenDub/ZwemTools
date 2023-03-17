// <copyright file="MauiProgram.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Radzen;
using ZwemTools.Abstractions;
using ZwemTools.Data.Lenex;
using ZwemTools.Data.Sql;
using ZwemTools.Data.TeamManager;

namespace ZwemTools;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
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
            builder.Services.AddSingleton<ITeamManagerDatabase, TeamManagerDatabase>();
            builder.Services.AddSingleton<RelaysService>();
        }

        builder.Services.AddRadzenServices();

        builder.Services.AddDbContext<DatabaseContext>();
        builder.Services.AddLogging(logging =>
        {
            logging.AddDebug();
            logging.AddEventLog();
        });

        builder.Services.AddScoped<IPreferenceService, PreferenceService>();

        MauiApp app = builder.Build();

        RunStartupTasks(app);

        return app;
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
        return services;
    }
}
