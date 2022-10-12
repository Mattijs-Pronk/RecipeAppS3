using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_end_API.Migrations
{
    public partial class foreignEdited9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Users_userId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_userId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_userId",
                table: "Recipes",
                column: "userId",
                unique: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "Recipes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_userId",
                table: "Recipes",
                column: "userId",
                unique: true,
                filter: "[userId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Users_userId",
                table: "Recipes",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId");
        }
    }
}
