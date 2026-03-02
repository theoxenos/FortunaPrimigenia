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
            new Category { CategoryId = 1, CategoryName = "Food", IsActive = true },
            new Category { CategoryId = 2, CategoryName = "Fuel", IsActive = true },
            new Category { CategoryId = 3, CategoryName = "Entertainment", IsActive = true },
            new Category { CategoryId = 4, CategoryName = "Groceries", IsActive = true },
            new Category { CategoryId = 5, CategoryName = "Health", IsActive = true },
            new Category { CategoryId = 6, CategoryName = "Education", IsActive = true },
            new Category { CategoryId = 7, CategoryName = "Household Goods", IsActive = true },
            new Category { CategoryId = 8, CategoryName = "Other", IsActive = true },
            new Category { CategoryId = 9, CategoryName = "Income", IsActive = true }
        );
    }
}