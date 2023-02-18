using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTrade.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CoefficientsNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FirstCcyId",
                table: "coefficients",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SecondCcyId",
                table: "coefficients",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_coefficients_FirstCcyId",
                table: "coefficients",
                column: "FirstCcyId");

            migrationBuilder.CreateIndex(
                name: "IX_coefficients_SecondCcyId",
                table: "coefficients",
                column: "SecondCcyId");

            migrationBuilder.AddForeignKey(
                name: "FK_coefficients_currencies_FirstCcyId",
                table: "coefficients",
                column: "FirstCcyId",
                principalTable: "currencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_coefficients_currencies_SecondCcyId",
                table: "coefficients",
                column: "SecondCcyId",
                principalTable: "currencies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_coefficients_currencies_FirstCcyId",
                table: "coefficients");

            migrationBuilder.DropForeignKey(
                name: "FK_coefficients_currencies_SecondCcyId",
                table: "coefficients");

            migrationBuilder.DropIndex(
                name: "IX_coefficients_FirstCcyId",
                table: "coefficients");

            migrationBuilder.DropIndex(
                name: "IX_coefficients_SecondCcyId",
                table: "coefficients");

            migrationBuilder.DropColumn(
                name: "FirstCcyId",
                table: "coefficients");

            migrationBuilder.DropColumn(
                name: "SecondCcyId",
                table: "coefficients");
        }
    }
}
