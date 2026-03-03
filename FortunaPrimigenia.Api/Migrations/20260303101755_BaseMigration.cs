using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FortunaPrimigenia.Api.Migrations
{
    /// <inheritdoc />
    public partial class BaseMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false),
                    Currency = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    IsOnBudget = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryName = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Payee = table.Column<string>(type: "TEXT", nullable: false),
                    Memo = table.Column<string>(type: "TEXT", nullable: false),
                    InflowAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    OutflowAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsCleared = table.Column<bool>(type: "INTEGER", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "DisplayOrder", "IsActive", "ModifiedDate" },
                values: new object[,]
                {
                    { 1, "Food", new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc), 0, true, new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "Fuel", new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc), 0, true, new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "Entertainment", new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc), 0, true, new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, "Groceries", new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc), 0, true, new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, "Health", new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc), 0, true, new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, "Education", new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc), 0, true, new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, "Household Goods", new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc), 0, true, new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, "Other", new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc), 0, true, new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, "Income This Month", new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc), 0, true, new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, "Income Next Month", new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc), 0, true, new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
