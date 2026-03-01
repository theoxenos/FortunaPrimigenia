namespace FortunaPrimigenia.Api.Models;

public record Transaction
{
    public int TransactionId { get; init; }
    public int AccountId { get; init; }
    public Account Account { get; set; } = null!;
    public int CategoryId { get; init; }
    public Category Category { get; set; } = null!;
    public string Payee { get; init; } = string.Empty;
    public string Memo { get; init; } = string.Empty;
    public decimal InflowAmount { get; init; }
    public decimal OutflowAmount { get; init; }
    public bool IsCleared { get; init; }
    public DateTime TransactionDate { get; init; }
    public DateTime AvailableDate { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime ModifiedDate { get; init; }
}