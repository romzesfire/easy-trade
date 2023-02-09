using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EasyTrade.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TradesDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_currencies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsoCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_brokerTrades",
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
                    table.PrimaryKey("PK__brokerTrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK__brokerTrades__currencies_BuyCcyId",
                        column: x => x.BuyCcyId,
                        principalTable: "_currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__brokerTrades__currencies_SellCcyId",
                        column: x => x.SellCcyId,
                        principalTable: "_currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_clientTrades",
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
                    table.PrimaryKey("PK__clientTrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK__clientTrades__brokerTrades_BrokerCurrencyTradeId",
                        column: x => x.BrokerCurrencyTradeId,
                        principalTable: "_brokerTrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__clientTrades__currencies_BuyCcyId",
                        column: x => x.BuyCcyId,
                        principalTable: "_currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__clientTrades__currencies_SellCcyId",
                        column: x => x.SellCcyId,
                        principalTable: "_currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__brokerTrades_BuyCcyId",
                table: "_brokerTrades",
                column: "BuyCcyId");

            migrationBuilder.CreateIndex(
                name: "IX__brokerTrades_SellCcyId",
                table: "_brokerTrades",
                column: "SellCcyId");

            migrationBuilder.CreateIndex(
                name: "IX__clientTrades_BrokerCurrencyTradeId",
                table: "_clientTrades",
                column: "BrokerCurrencyTradeId");

            migrationBuilder.CreateIndex(
                name: "IX__clientTrades_BuyCcyId",
                table: "_clientTrades",
                column: "BuyCcyId");

            migrationBuilder.CreateIndex(
                name: "IX__clientTrades_SellCcyId",
                table: "_clientTrades",
                column: "SellCcyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_clientTrades");

            migrationBuilder.DropTable(
                name: "_brokerTrades");

            migrationBuilder.DropTable(
                name: "_currencies");
        }
    }
}
