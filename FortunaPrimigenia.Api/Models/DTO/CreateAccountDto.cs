namespace FortunaPrimigenia.Api.Models.DTO;

public record CreateAccountDto
{
    public required string Name { get; set; }
    public required decimal Balance { get; set; }
    public required string Currency { get; set; }
    public required string Type { get; set; }
    public bool IsOnBudget { get; set; }
}