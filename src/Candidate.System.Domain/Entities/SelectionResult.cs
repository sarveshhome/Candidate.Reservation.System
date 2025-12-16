using System.ComponentModel.DataAnnotations;
using Candidate.System.Domain.Enums;

namespace Candidate.System.Domain.Entities;

public class SelectionResult
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string CandidateId { get; set; } = string.Empty;
    
    [Required]
    public CandidateCategory Category { get; set; }
    
    [Required]
    public decimal Marks { get; set; }
    
    [Required]
    public decimal CutoffMark { get; set; }
    
    [Required]
    public bool IsSelected { get; set; }
    
    public string SelectionReason { get; set; } = string.Empty;
    
    [Required]
    public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
    
    public int Rank { get; set; }
}