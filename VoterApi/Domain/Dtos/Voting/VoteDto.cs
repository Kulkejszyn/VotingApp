using Domain.Dtos.User;

namespace Domain.Dtos.Voting;

public sealed record VoteDto
{
    public required UserDto VotingUser { get; init; }
    public required UserDto VotedUser { get; init; }
}
