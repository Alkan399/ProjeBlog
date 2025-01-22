using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjeBlog.Migrations
{
    public partial class ContentStatistics2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentStatistics_Contents_ContentID",
                table: "ContentStatistics");

            migrationBuilder.AlterColumn<int>(
                name: "ContentID",
                table: "ContentStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentStatistics_Contents_ContentID",
                table: "ContentStatistics",
                column: "ContentID",
                principalTable: "Contents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentStatistics_Contents_ContentID",
                table: "ContentStatistics");

            migrationBuilder.AlterColumn<int>(
                name: "ContentID",
                table: "ContentStatistics",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentStatistics_Contents_ContentID",
                table: "ContentStatistics",
                column: "ContentID",
                principalTable: "Contents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
