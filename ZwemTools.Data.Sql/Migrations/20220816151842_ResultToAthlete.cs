// <copyright file="20220816151842_ResultToAthlete.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZwemTools.Data.Sql.Migrations
{
    public partial class ResultToAthlete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Athletes_AthleteId",
                table: "Results");

            migrationBuilder.AlterColumn<int>(
                name: "AthleteId",
                table: "Results",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Athletes_AthleteId",
                table: "Results",
                column: "AthleteId",
                principalTable: "Athletes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Athletes_AthleteId",
                table: "Results");

            migrationBuilder.AlterColumn<int>(
                name: "AthleteId",
                table: "Results",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Athletes_AthleteId",
                table: "Results",
                column: "AthleteId",
                principalTable: "Athletes",
                principalColumn: "Id");
        }
    }
}
