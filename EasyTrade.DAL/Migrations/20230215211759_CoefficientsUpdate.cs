using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTrade.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CoefficientsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Operation",
                table: "coefficients");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTime",
                table: "coefficients",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTime",
                table: "balances",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "coefficients");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "balances");

            migrationBuilder.AddColumn<int>(
                name: "Operation",
                table: "coefficients",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
