using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTrade.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIsoType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CurrencyIso",
                table: "Operations",
                type: "char(3)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "IsoCode",
                table: "Currencies",
                type: "char(3)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "SecondCcyIsoCode",
                table: "Coefficients",
                type: "char(3)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstCcyIsoCode",
                table: "Coefficients",
                type: "char(3)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SellCurrencyIso",
                table: "ClientTrades",
                type: "char(3)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BuyCurrencyIso",
                table: "ClientTrades",
                type: "char(3)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "SellCurrencyIso",
                table: "BrokerTrades",
                type: "char(3)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BuyCurrencyIso",
                table: "BrokerTrades",
                type: "char(3)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyIso",
                table: "Balances",
                type: "char(3)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CurrencyIso",
                table: "Operations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(3)");

            migrationBuilder.AlterColumn<string>(
                name: "IsoCode",
                table: "Currencies",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(3)");

            migrationBuilder.AlterColumn<string>(
                name: "SecondCcyIsoCode",
                table: "Coefficients",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(3)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstCcyIsoCode",
                table: "Coefficients",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(3)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SellCurrencyIso",
                table: "ClientTrades",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(3)");

            migrationBuilder.AlterColumn<string>(
                name: "BuyCurrencyIso",
                table: "ClientTrades",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(3)");

            migrationBuilder.AlterColumn<string>(
                name: "SellCurrencyIso",
                table: "BrokerTrades",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(3)");

            migrationBuilder.AlterColumn<string>(
                name: "BuyCurrencyIso",
                table: "BrokerTrades",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(3)");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyIso",
                table: "Balances",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(3)");
        }
    }
}
