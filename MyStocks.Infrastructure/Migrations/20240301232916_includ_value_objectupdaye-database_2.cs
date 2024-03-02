using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyStocks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class includ_value_objectupdayedatabase_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AssociatedShares",
                table: "AssociatedShares");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AssociatedShares");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssociatedShares",
                table: "AssociatedShares",
                columns: new[] { "PortfolioId", "ShareId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AssociatedShares",
                table: "AssociatedShares");

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
    }
}
