using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioTrackerServer.Migrations;

/// <inheritdoc />
public partial class UserSettings2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "UserSettings",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                ColorScheme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                InvestingGoals = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PreferedLanguage = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserSettings", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "UserSettings");
    }
}
