using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actuals",
                columns: table => new
                {
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    Population = table.Column<decimal>(type: "Decimal(18,10)", nullable: false),
                    Households = table.Column<decimal>(type: "Decimal(18,10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actuals", x => x.State);
                });

            migrationBuilder.CreateTable(
                name: "Estimates",
                columns: table => new
                {
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    District = table.Column<int>(type: "INTEGER", nullable: false),
                    Population = table.Column<decimal>(type: "Decimal(18,10)", nullable: false),
                    Households = table.Column<decimal>(type: "Decimal(18,10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estimates", x => new { x.State, x.District });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actuals");

            migrationBuilder.DropTable(
                name: "Estimates");
        }
    }
}
