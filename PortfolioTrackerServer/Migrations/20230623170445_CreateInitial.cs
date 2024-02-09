using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioTrackerServer.Migrations;

/// <inheritdoc />
public partial class CreateInitial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Stocks",
            columns: table => new
            {
                Ticker = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                PositionSize = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                SharesOwned = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                BuyInPrice = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                DividendYield = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                RelativePerformance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                AbsolutePerformance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                Industry = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Stocks", x => x.Ticker);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Stocks");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
