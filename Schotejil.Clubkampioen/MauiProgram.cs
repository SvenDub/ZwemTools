using Microsoft.AspNetCore.Components.WebView.Maui;
using Schotejil.Clubkampioen.Data;
using Schotejil.Clubkampioen.Data.Lenex;

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

        return builder.Build();
    }
}
