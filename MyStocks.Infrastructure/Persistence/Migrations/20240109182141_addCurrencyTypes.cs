using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStocks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCurrencyTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrencyTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CurrencyCode = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyTypes_Code",
                table: "CurrencyTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyTypes_CurrencyCode",
                table: "CurrencyTypes",
                column: "CurrencyCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyTypes");
        }
    }
}
