using Application.Persistence;
using Domain.Dtos.User;
using Domain.Dtos.Voting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Voting.Queries.GetAllVotes;

public sealed record GetAllVotesQuery : IRequest<IList<VoteDto>>;

public sealed class GetAllVotesQueryHandler : IRequestHandler<GetAllVotesQuery, IList<VoteDto>>
{
    private readonly IApplicationContext _context;

    public GetAllVotesQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<IList<VoteDto>> Handle(GetAllVotesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Vote.Select(vote => new VoteDto
        {
            VotedUser = new UserDto
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
        }).ToListAsync(cancellationToken);
    }
}
