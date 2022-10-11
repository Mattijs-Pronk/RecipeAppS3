using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_end_API.Migrations
{
    public partial class foreignEdited5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Users_userId",
                table: "Recipes");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "Recipes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Users_userId",
                table: "Recipes",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId");
        }
    }
}
