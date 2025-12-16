using Candidate.System.Application.DTOs;
using Candidate.System.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Candidate.System.Application.Services;

public class StreamingService : IStreamingService
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ISelectionService _selectionService;
    private readonly ILogger<StreamingService> _logger;
    private readonly Timer? _processingTimer;
    private readonly Queue<CandidateDto> _candidateQueue = new();
    private readonly object _queueLock = new();
    private bool _isRunning;

    public event EventHandler<IEnumerable<SelectionResultDto>>? ResultsProcessed;

    public StreamingService(
        ICandidateRepository candidateRepository,
        ISelectionService selectionService, 
        ILogger<StreamingService> logger)
    {
        _candidateRepository = candidateRepository;
        _selectionService = selectionService;
        _logger = logger;
    }

    public async Task StartStreamingAsync(CancellationToken cancellationToken = default)
    {
        if (_isRunning)
        {
            _logger.LogWarning("Streaming service is already running");
            return;
        }

        _isRunning = true;
        _logger.LogInformation("Starting streaming service with 30-second batch processing");

        var timer = new Timer(async _ => await ProcessQueuedCandidatesAsync(), null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        
        await Task.CompletedTask;
    }

    public async Task StopStreamingAsync()
    {
        if (!_isRunning)
            return;

        _isRunning = false;
        _processingTimer?.Dispose();
        
        await ProcessQueuedCandidatesAsync();
        _logger.LogInformation("Streaming service stopped");
    }

    public async Task ProcessCandidateBatchAsync(IEnumerable<CandidateDto> candidates)
    {
        lock (_queueLock)
        {
            foreach (var candidate in candidates)
            {
                _candidateQueue.Enqueue(candidate);
            }
        }

        _logger.LogInformation($"Added {candidates.Count()} candidates to processing queue");
        await Task.CompletedTask;
    }

    private async Task ProcessQueuedCandidatesAsync()
    {
        List<CandidateDto> candidatesToProcess;

        lock (_queueLock)
        {
            if (_candidateQueue.Count == 0)
                return;

            candidatesToProcess = new List<CandidateDto>();
            while (_candidateQueue.Count > 0)
            {
                candidatesToProcess.Add(_candidateQueue.Dequeue());
            }
        }

        try
        {
            _logger.LogInformation($"Processing batch of {candidatesToProcess.Count} candidates");
            
            // Save candidates to database
            var candidateEntities = candidatesToProcess.Select(dto => new Domain.Entities.Candidate
            {
                CandidateId = dto.CandidateId,
                CandidateName = dto.CandidateName,
                Category = dto.Category,
                Marks = dto.Marks,
                Timestamp = dto.Timestamp
            });
            
            await _candidateRepository.AddRangeAsync(candidateEntities);
            
            var results = await _selectionService.ProcessCandidatesAsync(candidatesToProcess);
            
            ResultsProcessed?.Invoke(this, results);
            
            _logger.LogInformation($"Successfully processed and saved {results.Count()} candidates");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing candidate batch");
        }
    }
}