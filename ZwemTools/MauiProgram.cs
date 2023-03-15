﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Radzen;
using ZwemTools.Data.Lenex;
using ZwemTools.Data.Sql;
using ZwemTools.Data.TeamManager;

namespace ZwemTools;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

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

        MauiApp app = builder.Build();

        using (IServiceScope scope = app.Services.CreateScope())
        {
            DatabaseContext db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            db.Database.Migrate();
        }

        return app;
    }

    public static IServiceCollection AddRadzenServices(this IServiceCollection services)
    {
        services.AddScoped<DialogService>();
        services.AddScoped<NotificationService>();
        return services;
    }
}