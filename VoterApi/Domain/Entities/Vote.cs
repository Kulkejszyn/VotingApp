namespace Domain.Entities;

public sealed class Vote
{
    public long VoteId { get; set; }
    public User VotingUser { get; set; }
    public long VotingUserId { get; set; }
    public User VotedUser { get; set; }
    public long VotedUserId { get; set; }
}