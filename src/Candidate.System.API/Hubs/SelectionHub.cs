using Microsoft.AspNetCore.SignalR;
using Candidate.System.Application.DTOs;

namespace Candidate.System.API.Hubs;

public class SelectionHub : Hub
{
    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}

public interface ISelectionClient
{
    Task ReceiveResults(IEnumerable<SelectionResultDto> results);
    Task ReceiveCutoffs(Dictionary<string, decimal> cutoffs);
}