namespace FortunaPrimigenia.Api.Models.DTO;

public record CreateTransactionDto
{
    public int AccountId { get; init; }
    public int CategoryId { get; init; }
    public string Payee { get; init; } = string.Empty;
    public string Memo { get; init; } = string.Empty;
    public decimal InflowAmount { get; init; }
    public decimal OutflowAmount { get; init; }
    public bool IsCleared { get; init; }
    public DateTime TransactionDate { get; init; }
}