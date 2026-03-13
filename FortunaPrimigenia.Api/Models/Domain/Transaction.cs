namespace FortunaPrimigenia.Api.Models.Domain;

public record Transaction
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; init; } = null!;
    public int CategoryId { get; set; }
    public Category Category { get; init; } = null!;
    public string Payee { get; set; } = string.Empty;
    public string Memo { get; set; } = string.Empty;
    public decimal InflowAmount { get; set; }
    public decimal OutflowAmount { get; set; }
    public bool IsCleared { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }
}