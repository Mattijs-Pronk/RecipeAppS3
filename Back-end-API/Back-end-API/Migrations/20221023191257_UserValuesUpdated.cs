using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_end_API.Migrations
{
    public partial class UserValuesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tokenExpires",
                table: "Users",
                newName: "passwordResetTokenExpires");

            migrationBuilder.RenameColumn(
                name: "activatetokenExpires",
                table: "Users",
                newName: "activateAccountTokenExpires");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "passwordResetTokenExpires",
                table: "Users",
                newName: "tokenExpires");

            migrationBuilder.RenameColumn(
                name: "activateAccountTokenExpires",
                table: "Users",
                newName: "activatetokenExpires");
        }
    }
}
