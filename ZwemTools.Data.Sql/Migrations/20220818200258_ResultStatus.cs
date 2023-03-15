// <copyright file="20220818200258_ResultStatus.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZwemTools.Data.Sql.Migrations
{
    public partial class ResultStatus : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Results",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Results");
        }
    }
}
