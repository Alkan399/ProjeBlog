using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjeBlog.Migrations
{
    public partial class basvurudesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Basvurus",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Basvurus");
        }
    }
}
