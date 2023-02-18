using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EasyTrade.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsoCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Balances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Balances_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrokerTrades",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    BuyAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    SellAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TradeType = table.Column<int>(type: "integer", nullable: false),
                    BuyCcyId = table.Column<long>(type: "bigint", nullable: false),
                    SellCcyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerTrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrokerTrades_Currencies_BuyCcyId",
                        column: x => x.BuyCcyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrokerTrades_Currencies_SellCcyId",
                        column: x => x.SellCcyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coefficients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstCcyId = table.Column<long>(type: "bigint", nullable: true),
                    SecondCcyId = table.Column<long>(type: "bigint", nullable: true),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Coefficient = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coefficients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coefficients_Currencies_FirstCcyId",
                        column: x => x.FirstCcyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Coefficients_Currencies_SecondCcyId",
                        column: x => x.SecondCcyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientTrades",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrokerCurrencyTradeId = table.Column<long>(type: "bigint", nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    BuyAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    SellAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TradeType = table.Column<int>(type: "integer", nullable: false),
                    BuyCcyId = table.Column<long>(type: "bigint", nullable: false),
                    SellCcyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientTrades_BrokerTrades_BrokerCurrencyTradeId",
                        column: x => x.BrokerCurrencyTradeId,
                        principalTable: "BrokerTrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientTrades_Currencies_BuyCcyId",
                        column: x => x.BuyCcyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientTrades_Currencies_SellCcyId",
                        column: x => x.SellCcyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Balances_CurrencyId",
                table: "Balances",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerTrades_BuyCcyId",
                table: "BrokerTrades",
                column: "BuyCcyId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerTrades_SellCcyId",
                table: "BrokerTrades",
                column: "SellCcyId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTrades_BrokerCurrencyTradeId",
                table: "ClientTrades",
                column: "BrokerCurrencyTradeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTrades_BuyCcyId",
                table: "ClientTrades",
                column: "BuyCcyId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTrades_SellCcyId",
                table: "ClientTrades",
                column: "SellCcyId");

            migrationBuilder.CreateIndex(
                name: "IX_Coefficients_FirstCcyId",
                table: "Coefficients",
                column: "FirstCcyId");

            migrationBuilder.CreateIndex(
                name: "IX_Coefficients_SecondCcyId",
                table: "Coefficients",
                column: "SecondCcyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balances");

            migrationBuilder.DropTable(
                name: "ClientTrades");

            migrationBuilder.DropTable(
                name: "Coefficients");

            migrationBuilder.DropTable(
                name: "BrokerTrades");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
