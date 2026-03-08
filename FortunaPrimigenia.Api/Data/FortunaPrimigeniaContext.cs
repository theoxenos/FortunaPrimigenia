using FortunaPrimigenia.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FortunaPrimigenia.Api.Data;

public class FortunaPrimigeniaContext(DbContextOptions<FortunaPrimigeniaContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData
        (
            new Category
            {
                Id = 1,
                CategoryName = "Food",
                IsActive = true,
                DisplayOrder = 1,
                CreatedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc),
                ModifiedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 2,
                CategoryName = "Fuel",
                IsActive = true,
                DisplayOrder = 2,
                CreatedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc),
                ModifiedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 3,
                CategoryName = "Entertainment",
                IsActive = true,
                DisplayOrder = 3,
                CreatedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc),
                ModifiedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 4,
                CategoryName = "Groceries",
                IsActive = true,
                DisplayOrder = 4,
                CreatedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc),
                ModifiedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 5,
                CategoryName = "Health",
                IsActive = true,
                DisplayOrder = 5,
                CreatedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc),
                ModifiedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 6,
                CategoryName = "Education",
                IsActive = true,
                DisplayOrder = 6,
                CreatedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc),
                ModifiedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 7,
                CategoryName = "Household Goods",
                IsActive = true,
                DisplayOrder = 7,
                CreatedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc),
                ModifiedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 8,
                CategoryName = "Other",
                IsActive = true,
                DisplayOrder = 8,
                CreatedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc),
                ModifiedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 9,
                CategoryName = "Income This Month",
                IsActive = true,
                DisplayOrder = 9,
                CreatedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc),
                ModifiedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                Id = 10,
                CategoryName = "Income Next Month",
                IsActive = true,
                DisplayOrder = 10,
                CreatedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc),
                ModifiedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Account>().HasData
        (
            new Account
            {
                Id = 1,
                Name = "ING Bank",
                Balance = 1_000,
                Currency = "EUR",
                Type = "Checking",
                CreatedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc),
                ModifiedDate = new DateTime(2026, 3, 3, 8, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Transaction>().HasData
        (
            DataSeeder.GenerateTransactions()
        );
    }
}