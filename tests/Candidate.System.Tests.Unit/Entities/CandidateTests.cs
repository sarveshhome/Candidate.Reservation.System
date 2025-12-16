using Candidate.System.Domain.Entities;
using Candidate.System.Domain.Enums;
using Xunit;

namespace Candidate.System.Tests.Unit.Entities;

public class CandidateTests
{
    [Fact]
    public void Candidate_ValidProperties_SetsCorrectly()
    {
        // Arrange & Act
        var candidate = new Domain.Entities.Candidate
        {
            CandidateId = "C001",
            CandidateName = "John Doe",
            Category = CandidateCategory.GENERAL,
            Marks = 85.5m,
            Timestamp = DateTime.UtcNow,
            IsSelected = true,
            SelectionReason = "Selected in GENERAL category"
        };

        // Assert
        Assert.Equal("C001", candidate.CandidateId);
        Assert.Equal("John Doe", candidate.CandidateName);
        Assert.Equal(CandidateCategory.GENERAL, candidate.Category);
        Assert.Equal(85.5m, candidate.Marks);
        Assert.True(candidate.IsSelected);
        Assert.Equal("Selected in GENERAL category", candidate.SelectionReason);
    }

    [Fact]
    public void Candidate_DefaultValues_AreCorrect()
    {
        // Arrange & Act
        var candidate = new Domain.Entities.Candidate();

        // Assert
        Assert.Equal(string.Empty, candidate.CandidateId);
        Assert.Equal(string.Empty, candidate.CandidateName);
        Assert.Equal(default(CandidateCategory), candidate.Category);
        Assert.Equal(0m, candidate.Marks);
        Assert.False(candidate.IsSelected);
        Assert.Null(candidate.SelectionReason);
    }

    [Theory]
    [InlineData(CandidateCategory.GENERAL)]
    [InlineData(CandidateCategory.OBC)]
    [InlineData(CandidateCategory.SC_ST)]
    [InlineData(CandidateCategory.WOMAN)]
    [InlineData(CandidateCategory.WOMAN_OBC)]
    [InlineData(CandidateCategory.WOMAN_SC_ST)]
    public void Candidate_AllCategories_CanBeSet(CandidateCategory category)
    {
        // Arrange & Act
        var candidate = new Domain.Entities.Candidate
        {
            Category = category
        };

        // Assert
        Assert.Equal(category, candidate.Category);
    }
}