using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjeBlog.Migrations
{
    public partial class nmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasvuruID",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_BasvuruID",
                table: "Users",
                column: "BasvuruID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Basvuru_BasvuruID",
                table: "Users",
                column: "BasvuruID",
                principalTable: "Basvuru",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Basvuru_BasvuruID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_BasvuruID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BasvuruID",
                table: "Users");
        }
    }
}
