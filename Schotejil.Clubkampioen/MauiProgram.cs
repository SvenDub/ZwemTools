using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Radzen;
using Schotejil.Clubkampioen.Data.Lenex;
using Schotejil.Clubkampioen.Data.Sql;
using Schotejil.Clubkampioen.Data.TeamManager;

namespace Schotejil.Clubkampioen;

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
        builder.Services.AddLogging(logging => logging.AddDebug());

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
