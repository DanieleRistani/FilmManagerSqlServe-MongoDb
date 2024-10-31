using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmManager.Migrations
{
    /// <inheritdoc />
    public partial class createDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Category_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category_img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category_description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Category_id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Log_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Log_errorMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Log_dateError = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Log_id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Tag_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag_description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Tag_id);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    Film_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Film_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Film_UrlImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Film_director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Film_relase_date = table.Column<DateOnly>(type: "date", nullable: false),
                    Film_category_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Film_id);
                    table.ForeignKey(
                        name: "FK_Films_Categories",
                        column: x => x.Film_category_id,
                        principalTable: "Categories",
                        principalColumn: "Category_id");
                });

            migrationBuilder.CreateTable(
                name: "Films_Tags_Pivot",
                columns: table => new
                {
                    Film_Tag_id = table.Column<long>(type: "bigint", nullable: false),
                    Film_Tag_Film_id = table.Column<long>(type: "bigint", nullable: false),
                    Film_Tag_Tag_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films_Tags_Pivot", x => x.Film_Tag_id);
                    table.ForeignKey(
                        name: "FK_Films_Tags_Pivot_Films_Film_Tag_id",
                        column: x => x.Film_Tag_Film_id,
                        principalTable: "Films",
                        principalColumn: "Film_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Films_Tags_Pivot_Tags_Film_Tag_id",
                        column: x => x.Film_Tag_Tag_id,
                        principalTable: "Tags",
                        principalColumn: "Tag_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Films_Film_category_id",
                table: "Films",
                column: "Film_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Films_Tags_Pivot_Film_Tag_Film_id",
                table: "Films_Tags_Pivot",
                column: "Film_Tag_Film_id");

            migrationBuilder.CreateIndex(
                name: "IX_Films_Tags_Pivot_Film_Tag_Tag_id",
                table: "Films_Tags_Pivot",
                column: "Film_Tag_Tag_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Films_Tags_Pivot");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
