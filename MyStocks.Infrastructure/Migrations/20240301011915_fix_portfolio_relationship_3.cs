using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStocks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix_portfolio_relationship_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssociatedShares_Portfolios_PortfolioId",
                table: "AssociatedShares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Portfolios",
                table: "Portfolios");

            migrationBuilder.RenameTable(
                name: "Portfolios",
                newName: "PortfolioShare");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PortfolioShare",
                table: "PortfolioShare",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssociatedShares_PortfolioShare_PortfolioId",
                table: "AssociatedShares",
                column: "PortfolioId",
                principalTable: "PortfolioShare",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssociatedShares_PortfolioShare_PortfolioId",
                table: "AssociatedShares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PortfolioShare",
                table: "PortfolioShare");

            migrationBuilder.RenameTable(
                name: "PortfolioShare",
                newName: "Portfolios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Portfolios",
                table: "Portfolios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssociatedShares_Portfolios_PortfolioId",
                table: "AssociatedShares",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
