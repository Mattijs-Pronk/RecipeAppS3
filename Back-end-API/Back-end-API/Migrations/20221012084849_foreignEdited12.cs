using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_end_API.Migrations
{
    public partial class foreignEdited12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipes_userId",
                table: "Recipes");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_userId",
                table: "Recipes",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipes_userId",
                table: "Recipes");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_userId",
                table: "Recipes",
                column: "userId",
                unique: true);
        }
    }
}
