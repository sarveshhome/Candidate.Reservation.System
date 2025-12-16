using System.ComponentModel.DataAnnotations;
using Candidate.System.Domain.Enums;

namespace Candidate.System.Domain.Entities;

public class Candidate
{
    [Key]
    public string CandidateId { get; set; } = string.Empty;
    
    [Required]
    public string CandidateName { get; set; } = string.Empty;
    
    [Required]
    public CandidateCategory Category { get; set; }
    
    [Required]
    public decimal Marks { get; set; }
    
    [Required]
    public DateTime Timestamp { get; set; }
    
    public bool IsSelected { get; set; }
    
    public string? SelectionReason { get; set; }
}