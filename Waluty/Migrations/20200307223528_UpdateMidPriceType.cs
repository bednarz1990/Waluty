using Microsoft.EntityFrameworkCore.Migrations;

namespace Waluty.Migrations
{
    public partial class UpdateMidPriceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MidPrice",
                table: "Rate",
                type: "decimal(18, 4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MidPrice",
                table: "Rate",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 4)");
        }
    }
}
