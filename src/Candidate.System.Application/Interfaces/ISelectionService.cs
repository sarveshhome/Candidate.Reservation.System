using Candidate.System.Application.DTOs;
using Candidate.System.Domain.Enums;

namespace Candidate.System.Application.Interfaces;

public interface ISelectionService
{
    Task<IEnumerable<SelectionResultDto>> ProcessCandidatesAsync(IEnumerable<CandidateDto> candidates);
    Task<decimal> CalculateCutoffAsync(CandidateCategory category, IEnumerable<CandidateDto> candidates);
    Task<Dictionary<CandidateCategory, decimal>> CalculateAllCutoffsAsync(IEnumerable<CandidateDto> candidates);
}