// <copyright file="Member.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.TeamManager;

public record Member
{
    [Column("MEMBERSID")]
    public required int Id { get; init; }

    [Column("LASTNAME")]
    public required string Lastname { get; set; }

    [Column("FIRSTNAME")]
    public required string Firstname { get; set; }

    [Column("BIRTHDATE")]
    public required DateTime Birthdate { get; set; }

    [Column("GROUPS")]
    public required string Groups { get; set; }

    /// <summary>
    /// Gets or sets the gender of the member.
    /// </summary>
    [Column("GENDER")]
    public required Gender Gender { get; set; }

    public IEnumerable<string> GroupNames => this.Groups.Split(",").Select(group => group.Trim());

    public string FullName => $"{this.Firstname} {this.Lastname}";
}
