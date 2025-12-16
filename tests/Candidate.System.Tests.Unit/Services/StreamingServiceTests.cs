using Candidate.System.Application.DTOs;
using Candidate.System.Application.Interfaces;
using Candidate.System.Application.Services;
using Candidate.System.Domain.Entities;
using Candidate.System.Domain.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Candidate.System.Tests.Unit.Services;

public class StreamingServiceTests
{
    private readonly Mock<ICandidateRepository> _mockRepository;
    private readonly Mock<ISelectionService> _mockSelectionService;
    private readonly Mock<IHubContext<Hub>> _mockHubContext;
    private readonly Mock<ILogger<StreamingService>> _mockLogger;
    private readonly StreamingService _streamingService;

    public StreamingServiceTests()
    {
        _mockRepository = new Mock<ICandidateRepository>();
        _mockSelectionService = new Mock<ISelectionService>();
        _mockHubContext = new Mock<IHubContext<Hub>>();
        _mockLogger = new Mock<ILogger<StreamingService>>();
        _streamingService = new StreamingService(
            _mockRepository.Object,
            _mockSelectionService.Object,
            _mockHubContext.Object,
            _mockLogger.Object);
    }

    [Fact]
    public async Task ProcessCandidateBatchAsync_ValidCandidates_ProcessesSuccessfully()
    {
        // Arrange
        var candidates = new List<CandidateDto>
        {
            new() { CandidateId = "C1", CandidateName = "Test", Category = CandidateCategory.GENERAL, Marks = 85 }
        };

        var selectionResults = new List<SelectionResultDto>
        {
            new() { CandidateId = "C1", IsSelected = true }
        };

        _mockSelectionService.Setup(x => x.ProcessCandidatesAsync(candidates))
            .ReturnsAsync(selectionResults);

        // Act
        await _streamingService.ProcessCandidateBatchAsync(candidates);

        // Assert
        _mockRepository.Verify(x => x.AddCandidatesAsync(It.IsAny<IEnumerable<Domain.Entities.Candidate>>()), Times.Once);
        _mockSelectionService.Verify(x => x.ProcessCandidatesAsync(candidates), Times.Once);
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
        _mockRepository.Verify(x => x.AddCandidatesAsync(It.IsAny<IEnumerable<Domain.Entities.Candidate>>()), Times.Never);
    }
}