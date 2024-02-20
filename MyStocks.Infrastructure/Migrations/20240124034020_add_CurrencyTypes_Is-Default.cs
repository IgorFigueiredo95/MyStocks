using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStocks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_CurrencyTypes_IsDefault : Migration
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
                    Name = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    ShareType = table.Column<int>(type: "integer", nullable: false),
                    TotalValueInvested_CurrencyTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalValueInvested_Value = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalShares = table.Column<decimal>(type: "numeric", nullable: false),
                    AveragePrice_CurrencyTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    AveragePrice_Value = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shares_CurrencyTypes_AveragePrice_CurrencyTypeId",
                        column: x => x.AveragePrice_CurrencyTypeId,
                        principalTable: "CurrencyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shares_CurrencyTypes_TotalValueInvested_CurrencyTypeId",
                        column: x => x.TotalValueInvested_CurrencyTypeId,
                        principalTable: "CurrencyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShareDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ShareId = table.Column<Guid>(type: "uuid", nullable: false),
                    Note = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    Price_CurrencyTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price_Value = table.Column<decimal>(type: "numeric", nullable: false),
                    OperandType = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ShareId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShareDetails_CurrencyTypes_Price_CurrencyTypeId",
                        column: x => x.Price_CurrencyTypeId,
                        principalTable: "CurrencyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShareDetails_Shares_ShareId",
                        column: x => x.ShareId,
                        principalTable: "Shares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShareDetails_Shares_ShareId1",
                        column: x => x.ShareId1,
                        principalTable: "Shares",
                        principalColumn: "Id");
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

            migrationBuilder.CreateIndex(
                name: "IX_ShareDetails_Price_CurrencyTypeId",
                table: "ShareDetails",
                column: "Price_CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareDetails_ShareId",
                table: "ShareDetails",
                column: "ShareId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareDetails_ShareId1",
                table: "ShareDetails",
                column: "ShareId1");

            migrationBuilder.CreateIndex(
                name: "IX_Shares_AveragePrice_CurrencyTypeId",
                table: "Shares",
                column: "AveragePrice_CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shares_Code",
                table: "Shares",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shares_TotalValueInvested_CurrencyTypeId",
                table: "Shares",
                column: "TotalValueInvested_CurrencyTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShareDetails");

            migrationBuilder.DropTable(
                name: "Shares");

            migrationBuilder.DropTable(
                name: "CurrencyTypes");
        }
    }
}
