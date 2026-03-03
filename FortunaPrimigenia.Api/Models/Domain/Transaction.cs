namespace FortunaPrimigenia.Api.Models.Domain;

public record Transaction
{
    public int Id { get; init; }
    public int AccountId { get; init; }
    public Account Account { get; init; } = null!;
    public int CategoryId { get; init; }
    public Category Category { get; init; } = null!;
    public string Payee { get; init; } = string.Empty;
    public string Memo { get; init; } = string.Empty;
    public decimal InflowAmount { get; init; }
    public decimal OutflowAmount { get; init; }
    public bool IsCleared { get; init; }
    public DateTime TransactionDate { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}