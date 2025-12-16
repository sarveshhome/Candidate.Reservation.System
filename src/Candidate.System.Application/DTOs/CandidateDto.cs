using Candidate.System.Domain.Enums;

namespace Candidate.System.Application.DTOs;

public class CandidateDto
{
    public string CandidateId { get; set; } = string.Empty;
    public string CandidateName { get; set; } = string.Empty;
    public CandidateCategory Category { get; set; }
    public decimal Marks { get; set; }
    public DateTime Timestamp { get; set; }
}

public class SelectionResultDto
{
    public string CandidateId { get; set; } = string.Empty;
    public string CandidateName { get; set; } = string.Empty;
    public CandidateCategory Category { get; set; }
    public decimal Marks { get; set; }
    public decimal CutoffMark { get; set; }
    public bool IsSelected { get; set; }
    public string SelectionReason { get; set; } = string.Empty;
    public int Rank { get; set; }
    public DateTime ProcessedAt { get; set; }
}