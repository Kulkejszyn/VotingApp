using Application.Features.Voting.Events;
using Application.Persistence;
using Domain.Dtos.User;
using Domain.Dtos.Voting;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Voting.Commands.AddVote;

public sealed record AddVoteCommand(long VotingUserId, long VotedUserId) : IRequest;

public sealed class AddVoteCommandHandler : IRequestHandler<AddVoteCommand>
{
    private readonly IApplicationContext _context;
    private readonly IMediator _mediator;

    public AddVoteCommandHandler(IApplicationContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(AddVoteCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequest(request, cancellationToken);

        var voteEntity = new Vote
        {
            VotingUserId = request.VotingUserId,
            VotedUserId = request.VotedUserId
        };
        
        _context.Vote.Add(voteEntity);
        await _context.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new VoteAddNotification(voteEntity.VoteId), cancellationToken);

        return Unit.Value;
    }

    private async Task ValidateRequest(AddVoteCommand request, CancellationToken cancellationToken)
    {
        if (request.VotedUserId == request.VotingUserId) throw new ForbiddenException("You can't vote for yourself");

        var doUsersExists = await _context.User.AnyAsync(p => p.Id == request.VotingUserId, cancellationToken) &&
                            await _context.User.AnyAsync(p => p.Id == request.VotedUserId, cancellationToken);
        if (!doUsersExists) throw new NotFoundException("User not found");
        
        var doUserAlreadyVoted = await _context.Vote.AnyAsync(p => p.VotingUserId == request.VotingUserId, cancellationToken);
        if (doUserAlreadyVoted) throw new ForbiddenException("You already voted");

        
    }
}
