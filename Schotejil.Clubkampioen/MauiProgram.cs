using Microsoft.EntityFrameworkCore;
using Schotejil.Clubkampioen.Data.Lenex;
using Schotejil.Clubkampioen.Data.Sql;

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

        builder.Services.AddDbContext<DatabaseContext>();

        MauiApp app = builder.Build();

        using (IServiceScope scope = app.Services.CreateScope())
        {
            DatabaseContext db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            db.Database.Migrate();
        }

        return app;
    }
}
