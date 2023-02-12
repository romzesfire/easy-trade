using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EasyTrade.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "coefficients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Operation = table.Column<int>(type: "integer", nullable: false),
                    Coefficient = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coefficients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "currencies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsoCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "balances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_balances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_balances_currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "brokerTrades",
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
                    table.PrimaryKey("PK_brokerTrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_brokerTrades_currencies_BuyCcyId",
                        column: x => x.BuyCcyId,
                        principalTable: "currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brokerTrades_currencies_SellCcyId",
                        column: x => x.SellCcyId,
                        principalTable: "currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clientTrades",
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
                    table.PrimaryKey("PK_clientTrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_clientTrades_brokerTrades_BrokerCurrencyTradeId",
                        column: x => x.BrokerCurrencyTradeId,
                        principalTable: "brokerTrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_clientTrades_currencies_BuyCcyId",
                        column: x => x.BuyCcyId,
                        principalTable: "currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_clientTrades_currencies_SellCcyId",
                        column: x => x.SellCcyId,
                        principalTable: "currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_balances_CurrencyId",
                table: "balances",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_brokerTrades_BuyCcyId",
                table: "brokerTrades",
                column: "BuyCcyId");

            migrationBuilder.CreateIndex(
                name: "IX_brokerTrades_SellCcyId",
                table: "brokerTrades",
                column: "SellCcyId");

            migrationBuilder.CreateIndex(
                name: "IX_clientTrades_BrokerCurrencyTradeId",
                table: "clientTrades",
                column: "BrokerCurrencyTradeId");

            migrationBuilder.CreateIndex(
                name: "IX_clientTrades_BuyCcyId",
                table: "clientTrades",
                column: "BuyCcyId");

            migrationBuilder.CreateIndex(
                name: "IX_clientTrades_SellCcyId",
                table: "clientTrades",
                column: "SellCcyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "balances");

            migrationBuilder.DropTable(
                name: "clientTrades");

            migrationBuilder.DropTable(
                name: "coefficients");

            migrationBuilder.DropTable(
                name: "brokerTrades");

            migrationBuilder.DropTable(
                name: "currencies");
        }
    }
}
