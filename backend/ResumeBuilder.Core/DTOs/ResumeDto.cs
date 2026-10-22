using System.ComponentModel.DataAnnotations;

namespace ResumeBuilder.Core.DTOs;

public class ResumeDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string ProfessionalSummary { get; set; } = string.Empty;
    public string Skills { get; set; } = string.Empty;
    public string Projects { get; set; } = string.Empty;
    public string Qualifications { get; set; } = string.Empty;
    public string References { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateResumeDto
{
    [Required(ErrorMessage = "Full name is required")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Job title is required")]
    public string JobTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Professional summary is required")]
    public string ProfessionalSummary { get; set; } = string.Empty;

    public string Skills { get; set; } = string.Empty;

    [Required(ErrorMessage = "Projects are required")]
    public string Projects { get; set; } = string.Empty;

    [Required(ErrorMessage = "Qualifications are required")]
    public string Qualifications { get; set; } = string.Empty;

    [Required(ErrorMessage = "References are required")]
    public string References { get; set; } = string.Empty;
}

public class UpdateResumeDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string ProfessionalSummary { get; set; } = string.Empty;
    public string Skills { get; set; } = string.Empty;
    public string Projects { get; set; } = string.Empty;
    public string Qualifications { get; set; } = string.Empty;
    public string References { get; set; } = string.Empty;
}
