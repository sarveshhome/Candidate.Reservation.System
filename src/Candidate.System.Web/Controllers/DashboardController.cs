using Candidate.System.Application.DTOs;
using Candidate.System.Application.Interfaces;
using Candidate.System.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Candidate.System.Web.Controllers;

public class DashboardController : Controller
{
    private readonly ISelectionService _selectionService;
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(ISelectionService selectionService, ILogger<DashboardController> logger)
    {
        _selectionService = selectionService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var viewModel = new DashboardViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ProcessCandidates([FromBody] List<CandidateInputViewModel> candidates)
    {
        try
        {
            var candidateDtos = candidates.Select(c => new CandidateDto
            {
                CandidateId = c.CandidateId,
                CandidateName = c.CandidateName,
                Category = c.Category,
                Marks = c.Marks,
                Timestamp = DateTime.UtcNow
            }).ToList();

            var results = await _selectionService.ProcessCandidatesAsync(candidateDtos);
            var cutoffs = await _selectionService.CalculateAllCutoffsAsync(candidateDtos);

            var viewModel = new DashboardViewModel
            {
                Results = results.ToList(),
                Cutoffs = cutoffs,
                TotalCandidates = results.Count(),
                SelectedCandidates = results.Count(r => r.IsSelected),
                LastUpdated = DateTime.UtcNow
            };

            return Json(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing candidates");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
}