using Domain.Dtos.Voting;

namespace Domain.Hubs;

public interface IVoteHub
{
    Task OnVoteAdd(VoteDto vote);
}
