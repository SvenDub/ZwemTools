using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schotejil.Clubkampioen.Data.Sql.Migrations
{
    public partial class ResultStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Results",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Results");
        }
    }
}
