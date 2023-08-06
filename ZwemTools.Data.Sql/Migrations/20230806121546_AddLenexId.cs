// <copyright file="20230806121546_AddLenexId.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZwemTools.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddLenexId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LenexEventId",
                table: "Results",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LenexId",
                table: "Results",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LenexResultId",
                table: "Rankings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Meets",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "LenexId",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "HeatId",
                table: "Entries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Lane",
                table: "Entries",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LenexEventId",
                table: "Entries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LenexHeatId",
                table: "Entries",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LenexId",
                table: "Clubs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LenexId",
                table: "Athletes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LenexId",
                table: "AgeGroups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Heat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    LenexId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventId = table.Column<int>(type: "INTEGER", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Heat_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entries_HeatId",
                table: "Entries",
                column: "HeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Heat_EventId",
                table: "Heat",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Heat_HeatId",
                table: "Entries",
                column: "HeatId",
                principalTable: "Heat",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Heat_HeatId",
                table: "Entries");

            migrationBuilder.DropTable(
                name: "Heat");

            migrationBuilder.DropIndex(
                name: "IX_Entries_HeatId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "LenexEventId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "LenexId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "LenexResultId",
                table: "Rankings");

            migrationBuilder.DropColumn(
                name: "LenexId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "HeatId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "Lane",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "LenexEventId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "LenexHeatId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "LenexId",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "LenexId",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "LenexId",
                table: "AgeGroups");

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
