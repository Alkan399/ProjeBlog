using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjeBlog.Migrations
{
    public partial class LogTypeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogType",
                table: "OperationsLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogType",
                table: "OperationsLogs");
        }
    }
}
