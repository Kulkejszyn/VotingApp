using Application.Features.Voting.Queries.GetAllVotes;
using Application.Hubs;
using Application.Persistence;
using Domain.Dtos.User;
using Domain.Dtos.Voting;
using Domain.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Voting.Events;

public record VoteAddNotification(long VoteId) : INotification;

public sealed class VoteAddNotificationHandler : INotificationHandler<VoteAddNotification>
{
    private readonly IHubContext<VoteHub, IVoteHub> _voteHub;
    private readonly IApplicationContext _context;

    public VoteAddNotificationHandler(IHubContext<VoteHub, IVoteHub> voteHub, IApplicationContext context)
    {
        _voteHub = voteHub;
        _context = context;
    }

    public async Task Handle(VoteAddNotification notification, CancellationToken cancellationToken)
    {
        var voteDto = await _context.Vote.Where(vote => vote.VoteId == notification.VoteId)
            .Select(vote => new VoteDto
            {
                VotedUser = new UserDto()
                {
                    Id = vote.VotedUser.Id,
                    Name = vote.VotedUser.Name,
                    Surname = vote.VotedUser.Surname
                },
                VotingUser = new UserDto
                {
                    Id = vote.VotingUser.Id,
                    Name = vote.VotingUser.Name,
                    Surname = vote.VotingUser.Surname
                }
            }).FirstAsync(cancellationToken);
        
        await _voteHub.Clients.All.OnVoteAdd(voteDto);
    }
}
