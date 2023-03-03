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
                    IsoCode = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.IsoCode);
                });

            migrationBuilder.CreateTable(
                name: "Balances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Version = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrencyIso = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Balances_Currencies_CurrencyIso",
                        column: x => x.CurrencyIso,
                        principalTable: "Currencies",
                        principalColumn: "IsoCode",
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
                    BuyCurrencyIso = table.Column<string>(type: "text", nullable: false),
                    SellCurrencyIso = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerTrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrokerTrades_Currencies_BuyCurrencyIso",
                        column: x => x.BuyCurrencyIso,
                        principalTable: "Currencies",
                        principalColumn: "IsoCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrokerTrades_Currencies_SellCurrencyIso",
                        column: x => x.SellCurrencyIso,
                        principalTable: "Currencies",
                        principalColumn: "IsoCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coefficients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstCcyIsoCode = table.Column<string>(type: "text", nullable: true),
                    SecondCcyIsoCode = table.Column<string>(type: "text", nullable: true),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Coefficient = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coefficients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coefficients_Currencies_FirstCcyIsoCode",
                        column: x => x.FirstCcyIsoCode,
                        principalTable: "Currencies",
                        principalColumn: "IsoCode");
                    table.ForeignKey(
                        name: "FK_Coefficients_Currencies_SecondCcyIsoCode",
                        column: x => x.SecondCcyIsoCode,
                        principalTable: "Currencies",
                        principalColumn: "IsoCode");
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CurrencyIso = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_Currencies_CurrencyIso",
                        column: x => x.CurrencyIso,
                        principalTable: "Currencies",
                        principalColumn: "IsoCode",
                        onDelete: ReferentialAction.Cascade);
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
                    BuyCurrencyIso = table.Column<string>(type: "text", nullable: false),
                    SellCurrencyIso = table.Column<string>(type: "text", nullable: false)
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
                        name: "FK_ClientTrades_Currencies_BuyCurrencyIso",
                        column: x => x.BuyCurrencyIso,
                        principalTable: "Currencies",
                        principalColumn: "IsoCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientTrades_Currencies_SellCurrencyIso",
                        column: x => x.SellCurrencyIso,
                        principalTable: "Currencies",
                        principalColumn: "IsoCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Balances_CurrencyIso",
                table: "Balances",
                column: "CurrencyIso");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerTrades_BuyCurrencyIso",
                table: "BrokerTrades",
                column: "BuyCurrencyIso");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerTrades_SellCurrencyIso",
                table: "BrokerTrades",
                column: "SellCurrencyIso");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTrades_BrokerCurrencyTradeId",
                table: "ClientTrades",
                column: "BrokerCurrencyTradeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTrades_BuyCurrencyIso",
                table: "ClientTrades",
                column: "BuyCurrencyIso");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTrades_SellCurrencyIso",
                table: "ClientTrades",
                column: "SellCurrencyIso");

            migrationBuilder.CreateIndex(
                name: "IX_Coefficients_FirstCcyIsoCode",
                table: "Coefficients",
                column: "FirstCcyIsoCode");

            migrationBuilder.CreateIndex(
                name: "IX_Coefficients_SecondCcyIsoCode",
                table: "Coefficients",
                column: "SecondCcyIsoCode");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_CurrencyIso",
                table: "Operations",
                column: "CurrencyIso");
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
                name: "Operations");

            migrationBuilder.DropTable(
                name: "BrokerTrades");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
