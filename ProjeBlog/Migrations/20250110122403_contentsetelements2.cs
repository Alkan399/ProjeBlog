using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjeBlog.Migrations
{
    public partial class contentsetelements2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContentSetsElements",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElementID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentSetID = table.Column<int>(type: "int", nullable: false),
                    Recent = table.Column<bool>(type: "bit", nullable: false),
                    MostPopular = table.Column<bool>(type: "bit", nullable: false),
                    ShowCount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentSetsElements", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContentSetsElements_ContentSets_ContentSetID",
                        column: x => x.ContentSetID,
                        principalTable: "ContentSets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentSetsElements_ContentSetID",
                table: "ContentSetsElements",
                column: "ContentSetID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentSetsElements");
        }
    }
}
