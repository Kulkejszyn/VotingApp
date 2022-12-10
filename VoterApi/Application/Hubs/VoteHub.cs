using Domain.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs;

public class VoteHub : Hub<IVoteHub>
{
}
