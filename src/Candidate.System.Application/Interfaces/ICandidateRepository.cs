using Candidate.System.Domain.Entities;
using Candidate.System.Domain.Enums;

namespace Candidate.System.Application.Interfaces;

public interface ICandidateRepository : IRepository<Domain.Entities.Candidate>
{
    Task<IEnumerable<Domain.Entities.Candidate>> GetByCategoryAsync(CandidateCategory category);
    Task<IEnumerable<Domain.Entities.Candidate>> GetTopCandidatesAsync(int count);
    Task<decimal> GetCutoffMarkAsync(CandidateCategory category, int availableSeats);
}