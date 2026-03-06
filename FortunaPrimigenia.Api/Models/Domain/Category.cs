namespace FortunaPrimigenia.Api.Models.Domain;

public record Category
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public int DisplayOrder { get; init; }
    public DateTime CreatedDate { get; init; } = DateTime.UtcNow;
    public DateTime ModifiedDate { get; init; } = DateTime.UtcNow;
}