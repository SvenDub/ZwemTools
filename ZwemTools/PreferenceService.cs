// <copyright file="PreferenceService.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using ZwemTools.Abstractions;

namespace ZwemTools;

/// <summary>
/// Service for getting and setting preferences using <see cref="Preferences"/>.
/// </summary>
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Non-static members are easier to mock in tests.")]
[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global", Justification = "Non-static members are easier to mock in tests.")]
public class PreferenceService : IPreferenceService
{
    /// <inheritdoc/>
    public CultureInfo Language
    {
        get => new(Preferences.Get("language", CultureInfo.CurrentCulture.Name));
        set => Preferences.Set("language", value.Name);
    }

    /// <inheritdoc/>
    public string? TeamManagerFile
    {
        get => Preferences.Get("team_manager_file", null);
        set => Preferences.Set("team_manager_file", value);
    }
}
