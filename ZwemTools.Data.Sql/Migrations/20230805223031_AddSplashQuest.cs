// <copyright file="20230805223031_AddSplashQuest.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZwemTools.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddSplashQuest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Meets",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "SplashQuestMeets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MeetId = table.Column<int>(type: "INTEGER", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SplashQuestMeets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SplashQuestMeets_Meets_MeetId",
                        column: x => x.MeetId,
                        principalTable: "Meets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SplashQuestTeams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SplashQuestMeetId = table.Column<Guid>(type: "TEXT", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SplashQuestTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SplashQuestTeams_SplashQuestMeets_SplashQuestMeetId",
                        column: x => x.SplashQuestMeetId,
                        principalTable: "SplashQuestMeets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Stroke = table.Column<int>(type: "INTEGER", nullable: false),
                    AthleteId = table.Column<int>(type: "INTEGER", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => new { x.TeamId, x.Id });
                    table.ForeignKey(
                        name: "FK_Assignment_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignment_SplashQuestTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "SplashQuestTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AthleteTeam",
                columns: table => new
                {
                    AthletesId = table.Column<int>(type: "INTEGER", nullable: false),
                    SplashQuestTeamsId = table.Column<Guid>(type: "TEXT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleteTeam", x => new { x.AthletesId, x.SplashQuestTeamsId });
                    table.ForeignKey(
                        name: "FK_AthleteTeam_Athletes_AthletesId",
                        column: x => x.AthletesId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AthleteTeam_SplashQuestTeams_SplashQuestTeamsId",
                        column: x => x.SplashQuestTeamsId,
                        principalTable: "SplashQuestTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_AthleteId",
                table: "Assignment",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_Stroke_AthleteId",
                table: "Assignment",
                columns: new[] { "Stroke", "AthleteId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AthleteTeam_SplashQuestTeamsId",
                table: "AthleteTeam",
                column: "SplashQuestTeamsId");

            migrationBuilder.CreateIndex(
                name: "IX_SplashQuestMeets_MeetId",
                table: "SplashQuestMeets",
                column: "MeetId");

            migrationBuilder.CreateIndex(
                name: "IX_SplashQuestTeams_SplashQuestMeetId",
                table: "SplashQuestTeams",
                column: "SplashQuestMeetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "AthleteTeam");

            migrationBuilder.DropTable(
                name: "SplashQuestTeams");

            migrationBuilder.DropTable(
                name: "SplashQuestMeets");

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
