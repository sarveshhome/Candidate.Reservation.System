using Candidate.System.Application.DTOs;

namespace Candidate.System.Application.Interfaces;

public interface IStreamingService
{
    Task StartStreamingAsync(CancellationToken cancellationToken = default);
    Task StopStreamingAsync();
    Task ProcessCandidateBatchAsync(IEnumerable<CandidateDto> candidates);
    event EventHandler<IEnumerable<SelectionResultDto>>? ResultsProcessed;
}