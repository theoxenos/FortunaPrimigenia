namespace FortunaPrimigenia.Api.Models;

public record Account
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Balance { get; set; }
    public required string Currency { get; set; }
    public required string Type { get; set; }
    public bool IsOnBudget { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public List<Transaction> Transactions { get; set; } = [];
}