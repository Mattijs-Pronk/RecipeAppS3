using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class nameUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeaturedRecipesModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeaturedModel",
                table: "FeaturedModel");

            migrationBuilder.RenameTable(
                name: "FeaturedModel",
                newName: "Featured");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Featured",
                table: "Featured",
                column: "featuredId");

            migrationBuilder.CreateTable(
                name: "FeaturedRecipes",
                columns: table => new
                {
                    recipeId = table.Column<int>(type: "int", nullable: false),
                    featuredId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturedRecipes", x => new { x.recipeId, x.featuredId });
                    table.ForeignKey(
                        name: "FK_FeaturedRecipes_Featured_featuredId",
                        column: x => x.featuredId,
                        principalTable: "Featured",
                        principalColumn: "featuredId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeaturedRecipes_Recipes_recipeId",
                        column: x => x.recipeId,
                        principalTable: "Recipes",
                        principalColumn: "recipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeaturedRecipes_featuredId",
                table: "FeaturedRecipes",
                column: "featuredId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeaturedRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Featured",
                table: "Featured");

            migrationBuilder.RenameTable(
                name: "Featured",
                newName: "FeaturedModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeaturedModel",
                table: "FeaturedModel",
                column: "featuredId");

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
    }
}
