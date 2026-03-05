using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FortunaPrimigenia.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Balance", "CreatedDate", "Currency", "IsOnBudget", "ModifiedDate", "Name", "Type" },
                values: new object[] { 1, 1000m, new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", false, new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc), "ING Bank", "Checking" });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "CategoryId", "CreatedAt", "InflowAmount", "IsCleared", "Memo", "OutflowAmount", "Payee", "TransactionDate", "UpdatedAt" },
                values: new object[] { -1, 1, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, false, "Dinner for coming week", 100m, "Supermarket", new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
