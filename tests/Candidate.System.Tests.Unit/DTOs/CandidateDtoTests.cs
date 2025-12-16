using Candidate.System.Application.DTOs;
using Candidate.System.Domain.Enums;
using Xunit;

namespace Candidate.System.Tests.Unit.DTOs;

public class CandidateDtoTests
{
    [Fact]
    public void CandidateDto_ValidProperties_SetsCorrectly()
    {
        // Arrange & Act
        var dto = new CandidateDto
        {
            CandidateId = "C001",
            CandidateName = "Jane Smith",
            Category = CandidateCategory.WOMAN_OBC,
            Marks = 92.5m,
            Timestamp = DateTime.UtcNow
        };

        // Assert
        Assert.Equal("C001", dto.CandidateId);
        Assert.Equal("Jane Smith", dto.CandidateName);
        Assert.Equal(CandidateCategory.WOMAN_OBC, dto.Category);
        Assert.Equal(92.5m, dto.Marks);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(50.5)]
    [InlineData(100)]
    public void CandidateDto_ValidMarks_AcceptsCorrectly(decimal marks)
    {
        // Arrange & Act
        var dto = new CandidateDto { Marks = marks };

        // Assert
        Assert.Equal(marks, dto.Marks);
    }
}

public class SelectionResultDtoTests
{
    [Fact]
    public void SelectionResultDto_ValidProperties_SetsCorrectly()
    {
        // Arrange & Act
        var dto = new SelectionResultDto
        {
            CandidateId = "C001",
            CandidateName = "Test User",
            Category = CandidateCategory.SC_ST,
            Marks = 78.5m,
            CutoffMark = 75.0m,
            IsSelected = true,
            SelectionReason = "Selected in SC_ST category",
            Rank = 1,
            ProcessedAt = DateTime.UtcNow
        };

        // Assert
        Assert.Equal("C001", dto.CandidateId);
        Assert.Equal("Test User", dto.CandidateName);
        Assert.Equal(CandidateCategory.SC_ST, dto.Category);
        Assert.Equal(78.5m, dto.Marks);
        Assert.Equal(75.0m, dto.CutoffMark);
        Assert.True(dto.IsSelected);
        Assert.Equal("Selected in SC_ST category", dto.SelectionReason);
        Assert.Equal(1, dto.Rank);
    }

    [Fact]
    public void SelectionResultDto_NotSelected_PropertiesCorrect()
    {
        // Arrange & Act
        var dto = new SelectionResultDto
        {
            IsSelected = false,
            SelectionReason = "Below cutoff (80.00)"
        };

        // Assert
        Assert.False(dto.IsSelected);
        Assert.Contains("Below cutoff", dto.SelectionReason);
    }
}