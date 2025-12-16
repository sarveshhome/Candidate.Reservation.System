using Candidate.System.API.Controllers;
using Candidate.System.Application.DTOs;
using Candidate.System.Application.Interfaces;
using Candidate.System.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Candidate.System.Tests.Unit.Controllers;

public class CandidateControllerTests
{
    private readonly Mock<ICandidateRepository> _mockRepository;
    private readonly Mock<IStreamingService> _mockStreamingService;
    private readonly Mock<ISelectionService> _mockSelectionService;
    private readonly Mock<ILogger<CandidateController>> _mockLogger;
    private readonly CandidateController _controller;

    public CandidateControllerTests()
    {
        _mockRepository = new Mock<ICandidateRepository>();
        _mockStreamingService = new Mock<IStreamingService>();
        _mockSelectionService = new Mock<ISelectionService>();
        _mockLogger = new Mock<ILogger<CandidateController>>();
        _controller = new CandidateController(_mockRepository.Object, _mockStreamingService.Object, _mockSelectionService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task SubmitCandidates_ValidInput_ReturnsOk()
    {
        // Arrange
        var candidates = new List<CandidateDto>
        {
            new() { CandidateId = "C001", CandidateName = "John", Category = CandidateCategory.GENERAL, Marks = 85.5m }
        };

        // Act
        var result = await _controller.SubmitCandidates(candidates);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public async Task ProcessCandidates_ValidInput_ReturnsOk()
    {
        // Arrange
        var candidates = new List<CandidateDto>
        {
            new() { CandidateId = "C001", CandidateName = "John", Category = CandidateCategory.GENERAL, Marks = 85.5m }
        };
        var expectedResults = new List<SelectionResultDto>();
        _mockSelectionService.Setup(x => x.ProcessCandidatesAsync(candidates)).ReturnsAsync(expectedResults);

        // Act
        var result = await _controller.ProcessCandidates(candidates);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedResults, okResult.Value);
    }

    [Fact]
    public async Task StartStreaming_Success_ReturnsOk()
    {
        // Act
        var result = await _controller.StartStreaming();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public async Task SubmitCandidates_ServiceThrows_ReturnsInternalServerError()
    {
        // Arrange
        var candidates = new List<CandidateDto>();
        var mockService = new Mock<IStreamingService>();
        mockService.Setup(s => s.ProcessCandidateBatchAsync(candidates))
            .ThrowsAsync(new Exception("Service error"));
        var controller = new CandidateController(_mockRepository.Object, mockService.Object, _mockSelectionService.Object, _mockLogger.Object);

        // Act
        var result = await controller.SubmitCandidates(candidates);

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusResult.StatusCode);
    }
}