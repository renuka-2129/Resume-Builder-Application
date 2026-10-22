using System.ComponentModel.DataAnnotations;

namespace ResumeBuilder.Core.Entities;

public class Resume : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Only Gmail addresses are allowed")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string JobTitle { get; set; } = string.Empty;

    [Required]
    [StringLength(2000)]
    public string ProfessionalSummary { get; set; } = string.Empty;

    public string Skills { get; set; } = string.Empty;

    [Required]
    [StringLength(4000)]
    public string Projects { get; set; } = string.Empty;

    [Required]
    [StringLength(2000)]
    public string Qualifications { get; set; } = string.Empty;

    [Required]
    [StringLength(2000)]
    public string References { get; set; } = string.Empty;
}
