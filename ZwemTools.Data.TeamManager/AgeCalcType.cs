// <copyright file="AgeCalcType.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Data.TeamManager;

/// <summary>
/// Describes how the age is calculated.
/// </summary>
public enum AgeCalcType
{
    /// <summary>
    /// The age is calculated using the year of the meet and the year of birth only.
    /// </summary>
    Year = 0,

    /// <summary>
    /// The age is calculated exactly between the date and the birth date.
    /// </summary>
    Date = 1,

    /// <summary>
    /// Age calculation according the Portuguese federation.
    /// </summary>
    Por = 2,

    /// <summary>
    /// Calculation according the Quebec federation.
    /// </summary>
    CanFnq = 3,

    /// <summary>
    /// Calculation according the Luxembourg federation.
    /// </summary>
    Lux = 4,
}
