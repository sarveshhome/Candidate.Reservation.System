using Candidate.System.Application.DTOs;
using Candidate.System.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.System.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidateController : ControllerBase
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IStreamingService _streamingService;
    private readonly ISelectionService _selectionService;
    private readonly ILogger<CandidateController> _logger;

    public CandidateController(
        ICandidateRepository candidateRepository,
        IStreamingService streamingService,
        ISelectionService selectionService,
        ILogger<CandidateController> logger)
    {
        _candidateRepository = candidateRepository;
        _streamingService = streamingService;
        _selectionService = selectionService;
        _logger = logger;
    }

    [HttpPost("submit")]
    public async Task<IActionResult> SubmitCandidates([FromBody] IEnumerable<CandidateDto> candidates)
    {
        try
        {
            await _streamingService.ProcessCandidateBatchAsync(candidates);
            return Ok(new { message = "Candidates submitted for processing", count = candidates.Count() });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error submitting candidates");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessCandidates([FromBody] IEnumerable<CandidateDto> candidates)
    {
        try
        {
            // Save candidates to database
            var candidateEntities = candidates.Select(dto => new Domain.Entities.Candidate
            {
                CandidateId = dto.CandidateId,
                CandidateName = dto.CandidateName,
                Category = dto.Category,
                Marks = dto.Marks,
                Timestamp = dto.Timestamp
            });
            
            await _candidateRepository.AddRangeAsync(candidateEntities);
            
            var results = await _selectionService.ProcessCandidatesAsync(candidates);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing candidates");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpPost("start-streaming")]
    public async Task<IActionResult> StartStreaming()
    {
        try
        {
            await _streamingService.StartStreamingAsync();
            return Ok(new { message = "Streaming service started" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error starting streaming service");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpPost("stop-streaming")]
    public async Task<IActionResult> StopStreaming()
    {
        try
        {
            await _streamingService.StopStreamingAsync();
            return Ok(new { message = "Streaming service stopped" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error stopping streaming service");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
}