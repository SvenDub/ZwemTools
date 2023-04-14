// <copyright file="Member.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace ZwemTools.Data.TeamManager;

public record Member
{
    [Column("MEMBERSID")]
    required public int Id { get; init; }

    [Column("LASTNAME")]
    required public string Lastname { get; set; }

    [Column("FIRSTNAME")]
    required public string Firstname { get; set; }

    [Column("BIRTHDATE")]
    required public DateTime Birthdate { get; set; }

    [Column("GROUPS")]
    required public string Groups { get; set; }

    /// <summary>
    /// Gets or sets the gender of the member.
    /// </summary>
    [Column("GENDER")]
    required public Gender Gender { get; set; }

    public IEnumerable<string> GroupNames => this.Groups.Split(",").Select(group => group.Trim());

    public string FullName => $"{this.Firstname} {this.Lastname}";
}
