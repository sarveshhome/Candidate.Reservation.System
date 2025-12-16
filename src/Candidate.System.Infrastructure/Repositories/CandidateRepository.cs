using Candidate.System.Application.Interfaces;
using Candidate.System.Domain.Enums;
using Candidate.System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Candidate.System.Infrastructure.Repositories;

public class CandidateRepository : Repository<Domain.Entities.Candidate>, ICandidateRepository
{
    public CandidateRepository(CandidateDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Domain.Entities.Candidate>> GetByCategoryAsync(CandidateCategory category)
    {
        return await _dbSet
            .Where(c => c.Category == category)
            .OrderByDescending(c => c.Marks)
            .ToListAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Candidate>> GetTopCandidatesAsync(int count)
    {
        return await _dbSet
            .OrderByDescending(c => c.Marks)
            .Take(count)
            .ToListAsync();
    }

    public async Task<decimal> GetCutoffMarkAsync(CandidateCategory category, int availableSeats)
    {
        var candidates = await _dbSet
            .Where(c => c.Category == category)
            .OrderByDescending(c => c.Marks)
            .Take(availableSeats)
            .ToListAsync();

        return candidates.LastOrDefault()?.Marks ?? 0;
    }
}