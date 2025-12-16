using Candidate.System.Application.DTOs;
using Candidate.System.Domain.Enums;

namespace Candidate.System.Web.Models.ViewModels;

public class DashboardViewModel
{
    public List<SelectionResultDto> Results { get; set; } = new();
    public Dictionary<CandidateCategory, decimal> Cutoffs { get; set; } = new();
    public Dictionary<CandidateCategory, int> CategoryCounts { get; set; } = new();
    public int TotalCandidates { get; set; }
    public int SelectedCandidates { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}

public class CandidateInputViewModel
{
    public string CandidateId { get; set; } = string.Empty;
    public string CandidateName { get; set; } = string.Empty;
    public CandidateCategory Category { get; set; }
    public decimal Marks { get; set; }
}