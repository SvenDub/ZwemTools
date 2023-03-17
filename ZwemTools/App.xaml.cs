// <copyright file="App.xaml.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.Globalization;
using ZwemTools.Abstractions;

namespace ZwemTools;

/// <inheritdoc />
public partial class App : Application
{
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    /// <param name="preferenceService">The preferences.</param>
    public App(IPreferenceService preferenceService)
    {
        this.InitializeComponent();

        CultureInfo cultureInfo = preferenceService.Language;
        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        this.MainPage = new MainPage();
    }

    /// <inheritdoc/>
    protected override Window CreateWindow(IActivationState? activationState)
    {
        Window window = base.CreateWindow(activationState);
        window.Title = "ZwemTools";
        return window;
    }
}
