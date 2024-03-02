using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyStocks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class includ_value_object : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AssociatedShares",
                table: "AssociatedShares");

            migrationBuilder.DropIndex(
                name: "IX_AssociatedShares_PortfolioId",
                table: "AssociatedShares");

            migrationBuilder.RenameColumn(
                name: "SharedId",
                table: "AssociatedShares",
                newName: "ShareId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AssociatedShares",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssociatedShares",
                table: "AssociatedShares",
                columns: new[] { "PortfolioId", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AssociatedShares",
                table: "AssociatedShares");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AssociatedShares");

            migrationBuilder.RenameColumn(
                name: "ShareId",
                table: "AssociatedShares",
                newName: "SharedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssociatedShares",
                table: "AssociatedShares",
                columns: new[] { "SharedId", "PortfolioId" });

            migrationBuilder.CreateIndex(
                name: "IX_AssociatedShares_PortfolioId",
                table: "AssociatedShares",
                column: "PortfolioId");
        }
    }
}
