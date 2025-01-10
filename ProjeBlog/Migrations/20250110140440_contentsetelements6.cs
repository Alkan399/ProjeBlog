using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjeBlog.Migrations
{
    public partial class contentsetelements6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentSetsElements_ContentSets_ContentSetID",
                table: "ContentSetsElements");

            migrationBuilder.AlterColumn<int>(
                name: "ContentSetID",
                table: "ContentSetsElements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentSetsElements_ContentSets_ContentSetID",
                table: "ContentSetsElements",
                column: "ContentSetID",
                principalTable: "ContentSets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentSetsElements_ContentSets_ContentSetID",
                table: "ContentSetsElements");

            migrationBuilder.AlterColumn<int>(
                name: "ContentSetID",
                table: "ContentSetsElements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentSetsElements_ContentSets_ContentSetID",
                table: "ContentSetsElements",
                column: "ContentSetID",
                principalTable: "ContentSets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
