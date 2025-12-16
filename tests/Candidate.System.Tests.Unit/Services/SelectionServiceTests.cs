using Candidate.System.Application.DTOs;
using Candidate.System.Application.Services;
using Candidate.System.Domain.Enums;
using Xunit;

namespace Candidate.System.Tests.Unit.Services;

public class SelectionServiceTests
{
    private readonly SelectionService _selectionService;

    public SelectionServiceTests()
    {
        _selectionService = new SelectionService();
    }

    [Fact]
    public async Task CalculateCutoffAsync_WithValidCandidates_ReturnsCorrectCutoff()
    {
        // Arrange
        var candidates = new List<CandidateDto>
        {
            new() { CandidateId = "C1", CandidateName = "Test1", Category = CandidateCategory.GENERAL, Marks = 90 },
            new() { CandidateId = "C2", CandidateName = "Test2", Category = CandidateCategory.GENERAL, Marks = 85 },
            new() { CandidateId = "C3", CandidateName = "Test3", Category = CandidateCategory.GENERAL, Marks = 80 }
        };

        // Act
        var cutoff = await _selectionService.CalculateCutoffAsync(CandidateCategory.GENERAL, candidates);

        // Assert
        Assert.True(cutoff > 0);
    }

    [Fact]
    public async Task ProcessCandidatesAsync_WithValidCandidates_ReturnsSelectionResults()
    {
        // Arrange
        var candidates = new List<CandidateDto>
        {
            new() { CandidateId = "C1", CandidateName = "Test1", Category = CandidateCategory.GENERAL, Marks = 90 },
            new() { CandidateId = "C2", CandidateName = "Test2", Category = CandidateCategory.OBC, Marks = 85 }
        };

        // Act
        var results = await _selectionService.ProcessCandidatesAsync(candidates);

        // Assert
        Assert.NotNull(results);
        Assert.Equal(2, results.Count());
    }

    [Fact]
    public async Task ProcessCandidatesAsync_WithDualReservationCandidate_HandlesCorrectly()
    {
        // Arrange
        var candidates = new List<CandidateDto>
        {
            new() { CandidateId = "C1", CandidateName = "Test1", Category = CandidateCategory.WOMAN_OBC, Marks = 85 },
            new() { CandidateId = "C2", CandidateName = "Test2", Category = CandidateCategory.GENERAL, Marks = 90 }
        };

        // Act
        var results = await _selectionService.ProcessCandidatesAsync(candidates);

        // Assert
        Assert.NotNull(results);
        Assert.Contains(results, r => r.CandidateId == "C1");
    }

    [Fact]
    public async Task CalculateAllCutoffsAsync_WithValidCandidates_ReturnsAllCategories()
    {
        // Arrange
        var candidates = new List<CandidateDto>
        {
            new() { CandidateId = "C1", CandidateName = "Test1", Category = CandidateCategory.GENERAL, Marks = 90 }
        };

        // Act
        var cutoffs = await _selectionService.CalculateAllCutoffsAsync(candidates);

        // Assert
        Assert.NotNull(cutoffs);
        Assert.Equal(6, cutoffs.Count);
        Assert.True(cutoffs.ContainsKey(CandidateCategory.GENERAL));
    }

    [Fact]
    public async Task CalculateCutoffAsync_WithEmptyCandidates_ReturnsZero()
    {
        // Arrange
        var candidates = new List<CandidateDto>();

        // Act
        var cutoff = await _selectionService.CalculateCutoffAsync(CandidateCategory.GENERAL, candidates);

        // Assert
        Assert.Equal(0, cutoff);
    }
}