using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_end_API.Migrations
{
    public partial class imgRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_userId",
                table: "Recipes",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Users_userId",
                table: "Recipes",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Users_userId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_userId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Recipes");
        }
    }
}
