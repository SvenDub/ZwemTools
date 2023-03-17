// <copyright file="IPreferenceService.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.Globalization;

namespace ZwemTools.Abstractions;

/// <summary>
/// Service for getting and setting preferences.
/// </summary>
public interface IPreferenceService
{
    /// <summary>
    /// Gets or sets the configured language.
    /// </summary>
    CultureInfo Language { get; set; }

    /// <summary>
    /// Gets or sets the path to the Team Manager database.
    /// </summary>
    string? TeamManagerFile { get; set; }
}
