using FortunaPrimigenia.Api.Models.Domain;

namespace FortunaPrimigenia.Api.Data;

public static class DataSeeder
{
    public static IEnumerable<Transaction> GenerateTransactions()
    {
        var transactions = new List<Transaction>();
        var random = new Random(42); // Seed for reproducibility
        var transactionId = 1;

        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2025, 12, 31);
        var totalDays = (endDate - startDate).Days;

        int[] categoryIds = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

        foreach (var categoryId in categoryIds)
            for (var i = 0; i < 100; i++)
            {
                var date = startDate.AddDays(random.Next(totalDays));
                decimal inflow = 0;
                decimal outflow = 0;

                // Category 9 and 10 are income
                if (categoryId is 9 or 10)
                    inflow = random.Next(500, 3000);
                else
                    outflow = (decimal)(random.NextDouble() * 100 + 5);

                transactions.Add(new Transaction
                {
                    Id = transactionId++,
                    AccountId = 1,
                    CategoryId = categoryId,
                    TransactionDate = date,
                    InflowAmount = Math.Round(inflow, 2),
                    OutflowAmount = Math.Round(outflow, 2),
                    Payee = GetPayeeForCategory(categoryId, random),
                    Memo = $"Sample transaction {i + 1} for category {categoryId}",
                    IsCleared = true,
                    CreatedAt = date,
                    UpdatedAt = date
                });
            }

        return transactions;
    }

    private static string GetPayeeForCategory(int categoryId, Random random)
    {
        return categoryId switch
        {
            1 => random.Next(2) == 0 ? "McDonald's" : "Starbucks",
            2 => "Shell Gas Station",
            3 => "Netflix",
            4 => "Local Supermarket",
            5 => "Pharmacy",
            6 => "Udemy",
            7 => "IKEA",
            9 or 10 => "Employer Inc.",
            _ => "Generic Vendor"
        };
    }
}