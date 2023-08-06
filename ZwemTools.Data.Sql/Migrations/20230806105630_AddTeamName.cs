// <copyright file="20230806105630_AddTeamName.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZwemTools.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SplashQuestTeams",
                type: "TEXT",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Meets",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SplashQuestTeams");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Meets",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);
        }
    }
}
