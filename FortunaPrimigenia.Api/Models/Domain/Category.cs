namespace FortunaPrimigenia.Api.Models.Domain;

public record Category
{
    public int CategoryId { get; init; }
    public string CategoryName { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public DateTime CreatedDate { get; init; }
}