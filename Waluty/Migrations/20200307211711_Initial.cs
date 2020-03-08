using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waluty.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExchangeRate",
                columns: table => new
                {
                    ExchangeRateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Table = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRate", x => x.ExchangeRateId);
                });

            migrationBuilder.CreateTable(
                name: "Rate",
                columns: table => new
                {
                    RateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(nullable: true),
                    EffectiveDate = table.Column<DateTime>(nullable: false),
                    MidPrice = table.Column<decimal>(nullable: false),
                    ExchangeRateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rate", x => x.RateId);
                    table.ForeignKey(
                        name: "FK_Rate_ExchangeRate_ExchangeRateId",
                        column: x => x.ExchangeRateId,
                        principalTable: "ExchangeRate",
                        principalColumn: "ExchangeRateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rate_ExchangeRateId",
                table: "Rate",
                column: "ExchangeRateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rate");

            migrationBuilder.DropTable(
                name: "ExchangeRate");
        }
    }
}
