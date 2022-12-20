using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class m2mAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "activeSince",
                table: "Users",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FeaturedModel",
                columns: table => new
                {
                    featuredId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturedModel", x => x.featuredId);
                });

            migrationBuilder.CreateTable(
                name: "FeaturedRecipesModel",
                columns: table => new
                {
                    recipeId = table.Column<int>(type: "int", nullable: false),
                    featuredId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturedRecipesModel", x => new { x.recipeId, x.featuredId });
                    table.ForeignKey(
                        name: "FK_FeaturedRecipesModel_FeaturedModel_featuredId",
                        column: x => x.featuredId,
                        principalTable: "FeaturedModel",
                        principalColumn: "featuredId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeaturedRecipesModel_Recipes_recipeId",
                        column: x => x.recipeId,
                        principalTable: "Recipes",
                        principalColumn: "recipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeaturedRecipesModel_featuredId",
                table: "FeaturedRecipesModel",
                column: "featuredId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeaturedRecipesModel");

            migrationBuilder.DropTable(
                name: "FeaturedModel");

            migrationBuilder.AlterColumn<DateTime>(
                name: "activeSince",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);
        }
    }
}
