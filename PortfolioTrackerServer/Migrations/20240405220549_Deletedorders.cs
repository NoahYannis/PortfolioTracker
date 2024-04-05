using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioTrackerServer.Migrations;

/// <inheritdoc />
public partial class Deletedorders : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Orders");

        migrationBuilder.AlterColumn<decimal>(
            name: "CurrentPrice",
            table: "Stocks",
            type: "decimal(18,2)",
            nullable: true,
            oldClrType: typeof(decimal),
            oldType: "decimal(8,2)",
            oldNullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "CurrentPrice",
            table: "Stocks",
            type: "decimal(8,2)",
            nullable: true,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,2)",
            oldNullable: true);

        migrationBuilder.CreateTable(
            name: "Orders",
            columns: table => new
            {
                OrderNumber = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                OrderType = table.Column<int>(type: "int", nullable: false),
                PortfolioId = table.Column<int>(type: "int", nullable: true),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false),
                Ticker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orders", x => x.OrderNumber);
                table.ForeignKey(
                    name: "FK_Orders_Portfolios_PortfolioId",
                    column: x => x.PortfolioId,
                    principalTable: "Portfolios",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Orders_PortfolioId",
            table: "Orders",
            column: "PortfolioId");
    }
}
