using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioTrackerServer.Migrations;

/// <inheritdoc />
public partial class Portfolio : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "PortfolioId",
            table: "Stocks",
            type: "int",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "PortfolioId",
            table: "Orders",
            type: "int",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "Portfolios",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                TotalRelativePerfomance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                TotalAbsolutePerformance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Portfolios", x => x.Id);
                table.ForeignKey(
                    name: "FK_Portfolios_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Stocks_PortfolioId",
            table: "Stocks",
            column: "PortfolioId");

        migrationBuilder.CreateIndex(
            name: "IX_Orders_PortfolioId",
            table: "Orders",
            column: "PortfolioId");

        migrationBuilder.CreateIndex(
            name: "IX_Portfolios_UserId",
            table: "Portfolios",
            column: "UserId");

        migrationBuilder.AddForeignKey(
            name: "FK_Orders_Portfolios_PortfolioId",
            table: "Orders",
            column: "PortfolioId",
            principalTable: "Portfolios",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Stocks_Portfolios_PortfolioId",
            table: "Stocks",
            column: "PortfolioId",
            principalTable: "Portfolios",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Orders_Portfolios_PortfolioId",
            table: "Orders");

        migrationBuilder.DropForeignKey(
            name: "FK_Stocks_Portfolios_PortfolioId",
            table: "Stocks");

        migrationBuilder.DropTable(
            name: "Portfolios");

        migrationBuilder.DropIndex(
            name: "IX_Stocks_PortfolioId",
            table: "Stocks");

        migrationBuilder.DropIndex(
            name: "IX_Orders_PortfolioId",
            table: "Orders");

        migrationBuilder.DropColumn(
            name: "PortfolioId",
            table: "Stocks");

        migrationBuilder.DropColumn(
            name: "PortfolioId",
            table: "Orders");
    }
}
