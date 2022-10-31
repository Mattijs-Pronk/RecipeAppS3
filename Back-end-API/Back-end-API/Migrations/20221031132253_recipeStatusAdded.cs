using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_end_API.Migrations
{
    public partial class recipeStatusAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Recipes");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Recipes");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
