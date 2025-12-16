using Candidate.System.Application.DTOs;
using Candidate.System.Application.Interfaces;
using Candidate.System.Domain.Enums;

namespace Candidate.System.Application.Services;

public class SelectionService : ISelectionService
{
    private readonly Dictionary<CandidateCategory, decimal> _reservationConfig = new()
    {
        { CandidateCategory.GENERAL, 50m },
        { CandidateCategory.OBC, 27m },
        { CandidateCategory.SC_ST, 22.5m },
        { CandidateCategory.WOMAN, 33m },
        { CandidateCategory.WOMAN_OBC, 15m },
        { CandidateCategory.WOMAN_SC_ST, 7.5m }
    };

    public async Task<IEnumerable<SelectionResultDto>> ProcessCandidatesAsync(IEnumerable<CandidateDto> candidates)
    {
        var candidateList = candidates.ToList();
        var cutoffs = await CalculateAllCutoffsAsync(candidateList);
        var results = new List<SelectionResultDto>();

        foreach (var candidate in candidateList.OrderByDescending(c => c.Marks))
        {
            var isSelected = IsEligibleForSelection(candidate, cutoffs);
            var selectionReason = GetSelectionReason(candidate, cutoffs, isSelected);

            results.Add(new SelectionResultDto
            {
                CandidateId = candidate.CandidateId,
                CandidateName = candidate.CandidateName,
                Category = candidate.Category,
                Marks = candidate.Marks,
                CutoffMark = cutoffs[candidate.Category],
                IsSelected = isSelected,
                SelectionReason = selectionReason,
                Rank = results.Count + 1,
                ProcessedAt = DateTime.UtcNow
            });
        }

        return results;
    }

    public async Task<decimal> CalculateCutoffAsync(CandidateCategory category, IEnumerable<CandidateDto> candidates)
    {
        var categoryCandidates = candidates.Where(c => c.Category == category || IsEligibleForDualReservation(c, category))
                                          .OrderByDescending(c => c.Marks)
                                          .ToList();

        if (!categoryCandidates.Any()) return 0;

        var reservationPercentage = _reservationConfig[category];
        var totalCandidates = candidates.Count();
        var reservedSeats = (int)Math.Ceiling(totalCandidates * reservationPercentage / 100);
        
        if (categoryCandidates.Count <= reservedSeats)
            return categoryCandidates.Last().Marks;

        return categoryCandidates[reservedSeats - 1].Marks;
    }

    public async Task<Dictionary<CandidateCategory, decimal>> CalculateAllCutoffsAsync(IEnumerable<CandidateDto> candidates)
    {
        var cutoffs = new Dictionary<CandidateCategory, decimal>();
        
        foreach (var category in Enum.GetValues<CandidateCategory>())
        {
            cutoffs[category] = await CalculateCutoffAsync(category, candidates);
        }

        return cutoffs;
    }

    private bool IsEligibleForSelection(CandidateDto candidate, Dictionary<CandidateCategory, decimal> cutoffs)
    {
        if (candidate.Marks >= cutoffs[candidate.Category])
            return true;

        return IsEligibleForDualReservation(candidate, cutoffs);
    }

    private bool IsEligibleForDualReservation(CandidateDto candidate, Dictionary<CandidateCategory, decimal> cutoffs)
    {
        return candidate.Category switch
        {
            CandidateCategory.WOMAN_OBC => candidate.Marks >= cutoffs[CandidateCategory.WOMAN] || candidate.Marks >= cutoffs[CandidateCategory.OBC],
            CandidateCategory.WOMAN_SC_ST => candidate.Marks >= cutoffs[CandidateCategory.WOMAN] || candidate.Marks >= cutoffs[CandidateCategory.SC_ST],
            _ => false
        };
    }

    private bool IsEligibleForDualReservation(CandidateDto candidate, CandidateCategory targetCategory)
    {
        return candidate.Category switch
        {
            CandidateCategory.WOMAN_OBC => targetCategory == CandidateCategory.WOMAN || targetCategory == CandidateCategory.OBC,
            CandidateCategory.WOMAN_SC_ST => targetCategory == CandidateCategory.WOMAN || targetCategory == CandidateCategory.SC_ST,
            _ => candidate.Category == targetCategory
        };
    }

    private string GetSelectionReason(CandidateDto candidate, Dictionary<CandidateCategory, decimal> cutoffs, bool isSelected)
    {
        if (!isSelected)
            return $"Below cutoff ({cutoffs[candidate.Category]:F2})";

        if (candidate.Marks >= cutoffs[candidate.Category])
            return $"Selected in {candidate.Category} category";

        if (candidate.Category == CandidateCategory.WOMAN_OBC)
        {
            if (candidate.Marks >= cutoffs[CandidateCategory.WOMAN])
                return "Selected in WOMAN category (dual reservation)";
            if (candidate.Marks >= cutoffs[CandidateCategory.OBC])
                return "Selected in OBC category (dual reservation)";
        }

        if (candidate.Category == CandidateCategory.WOMAN_SC_ST)
        {
            if (candidate.Marks >= cutoffs[CandidateCategory.WOMAN])
                return "Selected in WOMAN category (dual reservation)";
            if (candidate.Marks >= cutoffs[CandidateCategory.SC_ST])
                return "Selected in SC_ST category (dual reservation)";
        }

        return "Selected";
    }
}