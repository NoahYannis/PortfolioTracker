using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioTrackerServer.Migrations;

/// <inheritdoc />
public partial class PerformanceUpdate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<decimal>(
            name: "AbsolutePerformance",
            table: "Stocks",
            type: "decimal(18,2)",
            nullable: true);

        migrationBuilder.AddColumn<decimal>(
            name: "RelativePerformance",
            table: "Stocks",
            type: "decimal(18,2)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "AbsolutePerformance",
            table: "Stocks");

        migrationBuilder.DropColumn(
            name: "RelativePerformance",
            table: "Stocks");
    }
}
