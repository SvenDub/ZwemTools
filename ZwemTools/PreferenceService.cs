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
    [SuppressMessage("csharpsquid", "S1075", Justification = "Well known default path.")]
    private const string DefaultTeamManagerFile = @"C:\ProgramData\Team Manager\Team.mdb";

    /// <inheritdoc/>
    public CultureInfo Language
    {
        get => new(Preferences.Get("language", CultureInfo.CurrentCulture.Name));
        set => Preferences.Set("language", value.Name);
    }

    /// <inheritdoc/>
    public string? TeamManagerFile
    {
        get => Preferences.Get("team_manager_file", OperatingSystem.IsWindows() && File.Exists(DefaultTeamManagerFile) ? DefaultTeamManagerFile : null);
        set => Preferences.Set("team_manager_file", value);
    }
}
