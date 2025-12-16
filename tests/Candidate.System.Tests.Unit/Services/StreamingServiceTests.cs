using Candidate.System.Application.DTOs;
using Candidate.System.Application.Interfaces;
using Candidate.System.Application.Services;
using Candidate.System.Domain.Entities;
using Candidate.System.Domain.Enums;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Candidate.System.Tests.Unit.Services;

public class StreamingServiceTests
{
    private readonly Mock<ICandidateRepository> _mockRepository;
    private readonly Mock<ISelectionService> _mockSelectionService;
    private readonly Mock<ILogger<StreamingService>> _mockLogger;
    private readonly StreamingService _streamingService;

    public StreamingServiceTests()
    {
        _mockRepository = new Mock<ICandidateRepository>();
        _mockSelectionService = new Mock<ISelectionService>();
        _mockLogger = new Mock<ILogger<StreamingService>>();
        _streamingService = new StreamingService(
            _mockRepository.Object,
            _mockSelectionService.Object,
            _mockLogger.Object);
    }

    [Fact]
    public async Task ProcessCandidateBatchAsync_ValidCandidates_QueuesSuccessfully()
    {
        // Arrange
        var candidates = new List<CandidateDto>
        {
            new() { CandidateId = "C1", CandidateName = "Test", Category = CandidateCategory.GENERAL, Marks = 85 }
        };

        // Act
        await _streamingService.ProcessCandidateBatchAsync(candidates);

        // Assert - ProcessCandidateBatchAsync only queues candidates, doesn't process them immediately
        _mockRepository.Verify(x => x.AddRangeAsync(It.IsAny<IEnumerable<Domain.Entities.Candidate>>()), Times.Never);
        _mockSelectionService.Verify(x => x.ProcessCandidatesAsync(It.IsAny<IEnumerable<CandidateDto>>()), Times.Never);
    }

    [Fact]
    public async Task StartStreamingAsync_StartsSuccessfully()
    {
        // Act
        await _streamingService.StartStreamingAsync();

        // Assert - Verify no exceptions thrown
        Assert.True(true);
    }

    [Fact]
    public async Task StopStreamingAsync_StopsSuccessfully()
    {
        // Act
        await _streamingService.StopStreamingAsync();

        // Assert - Verify no exceptions thrown
        Assert.True(true);
    }

    [Fact]
    public async Task ProcessCandidateBatchAsync_EmptyList_HandlesGracefully()
    {
        // Arrange
        var candidates = new List<CandidateDto>();

        // Act
        await _streamingService.ProcessCandidateBatchAsync(candidates);

        // Assert
        _mockRepository.Verify(x => x.AddRangeAsync(It.IsAny<IEnumerable<Domain.Entities.Candidate>>()), Times.Never);
    }
}