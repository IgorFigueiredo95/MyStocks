using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStocks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerid1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Shares_OwnerId",
                table: "Shares",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_OwnerId",
                table: "Portfolios",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyTypes_OwnerId",
                table: "CurrencyTypes",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyTypes_Users_OwnerId",
                table: "CurrencyTypes",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_Users_OwnerId",
                table: "Portfolios",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shares_Users_OwnerId",
                table: "Shares",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyTypes_Users_OwnerId",
                table: "CurrencyTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_Users_OwnerId",
                table: "Portfolios");

            migrationBuilder.DropForeignKey(
                name: "FK_Shares_Users_OwnerId",
                table: "Shares");

            migrationBuilder.DropIndex(
                name: "IX_Shares_OwnerId",
                table: "Shares");

            migrationBuilder.DropIndex(
                name: "IX_Portfolios_OwnerId",
                table: "Portfolios");

            migrationBuilder.DropIndex(
                name: "IX_CurrencyTypes_OwnerId",
                table: "CurrencyTypes");
        }
    }
}
