using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjeBlog.Migrations
{
    public partial class basvuruson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Basvuru");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Basvuru");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Basvuru",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Basvuru",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Basvuru",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Basvuru",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
