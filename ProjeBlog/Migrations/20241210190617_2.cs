using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjeBlog.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Basvuru_BasvuruID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Basvuru",
                table: "Basvuru");

            migrationBuilder.RenameTable(
                name: "Basvuru",
                newName: "Basvurus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Basvurus",
                table: "Basvurus",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Basvurus_BasvuruID",
                table: "Users",
                column: "BasvuruID",
                principalTable: "Basvurus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Basvurus_BasvuruID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Basvurus",
                table: "Basvurus");

            migrationBuilder.RenameTable(
                name: "Basvurus",
                newName: "Basvuru");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Basvuru",
                table: "Basvuru",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Basvuru_BasvuruID",
                table: "Users",
                column: "BasvuruID",
                principalTable: "Basvuru",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
