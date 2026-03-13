using System.ComponentModel.DataAnnotations;

namespace FortunaPrimigenia.Api.Models.DTO;

public record CreateAccountDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; set; }

    [Range(double.MinValue, double.MaxValue)]
    public required decimal Balance { get; set; }

    [Required]
    [StringLength(3, MinimumLength = 3)]
    public required string Currency { get; set; }

    [Required] public required string Type { get; set; }

    public bool IsOnBudget { get; set; }
}