using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioTrackerServer.Migrations
{
    /// <inheritdoc />
    public partial class DeletePreferredLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferedLanguage",
                table: "UserSettings");

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_UserId",
                table: "UserSettings",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettings_Users_UserId",
                table: "UserSettings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSettings_Users_UserId",
                table: "UserSettings");

            migrationBuilder.DropIndex(
                name: "IX_UserSettings_UserId",
                table: "UserSettings");

            migrationBuilder.AddColumn<string>(
                name: "PreferedLanguage",
                table: "UserSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
