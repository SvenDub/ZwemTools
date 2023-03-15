﻿// <copyright file="App.xaml.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace ZwemTools.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}