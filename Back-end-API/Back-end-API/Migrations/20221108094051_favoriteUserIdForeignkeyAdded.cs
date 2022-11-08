using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_end_API.Migrations
{
    public partial class favoriteUserIdForeignkeyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Favorites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_userId",
                table: "Favorites",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Users_userId",
                table: "Favorites",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Users_userId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_userId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Favorites");
        }
    }
}
