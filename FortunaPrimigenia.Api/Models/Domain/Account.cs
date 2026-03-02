using System.Text.Json.Serialization;

namespace FortunaPrimigenia.Api.Models.Domain;

public record Account
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Balance { get; set; }
    public required string Currency { get; set; }
    public required string Type { get; set; }
    public bool IsOnBudget { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore] public List<Transaction> Transactions { get; set; } = [];
}