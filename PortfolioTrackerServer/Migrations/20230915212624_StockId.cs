using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioTrackerServer.Migrations;

/// <inheritdoc />
public partial class StockId : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Stocks_Portfolios_PortfolioId",
            table: "Stocks");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Stocks",
            table: "Stocks");

        migrationBuilder.AlterColumn<int>(
            name: "PortfolioId",
            table: "Stocks",
            type: "int",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "int",
            oldNullable: true);

        migrationBuilder.AddColumn<int>(
            name: "Id",
            table: "Stocks",
            type: "int",
            nullable: false,
            defaultValue: 0)
            .Annotation("SqlServer:Identity", "1, 1");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Stocks",
            table: "Stocks",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Stocks_Portfolios_PortfolioId",
            table: "Stocks",
            column: "PortfolioId",
            principalTable: "Portfolios",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Stocks_Portfolios_PortfolioId",
            table: "Stocks");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Stocks",
            table: "Stocks");

        migrationBuilder.DropColumn(
            name: "Id",
            table: "Stocks");

        migrationBuilder.AlterColumn<int>(
            name: "PortfolioId",
            table: "Stocks",
            type: "int",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "int");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Stocks",
            table: "Stocks",
            column: "Ticker");

        migrationBuilder.AddForeignKey(
            name: "FK_Stocks_Portfolios_PortfolioId",
            table: "Stocks",
            column: "PortfolioId",
            principalTable: "Portfolios",
            principalColumn: "Id");
    }
}
