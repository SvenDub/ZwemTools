using System.Globalization;

namespace Schotejil.Clubkampioen;

public partial class App : Application
{
	public App()
	{
        this.InitializeComponent();

		string language = Preferences.Get("language", CultureInfo.CurrentCulture.Name) ?? CultureInfo.CurrentCulture.Name;
		CultureInfo cultureInfo = new(language);
        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        this.MainPage = new MainPage();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		Window window = base.CreateWindow(activationState);
		window.Title = "Schotejil.Clubkampioen";
		return window;
	}
}
