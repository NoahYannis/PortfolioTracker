using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioTrackerServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPortfolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_Users_UserId",
                table: "Portfolios");

            migrationBuilder.DropIndex(
                name: "IX_Portfolios_UserId",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "TotalAbsolutePerformance",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "TotalRelativePerfomance",
                table: "Portfolios");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Portfolios",
                newName: "PortfolioValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PortfolioValue",
                table: "Portfolios",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAbsolutePerformance",
                table: "Portfolios",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalRelativePerfomance",
                table: "Portfolios",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_UserId",
                table: "Portfolios",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_Users_UserId",
                table: "Portfolios",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
