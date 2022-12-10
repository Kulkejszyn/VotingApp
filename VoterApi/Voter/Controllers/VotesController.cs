using Application.Features.Voting.Commands.AddVote;
using Application.Features.Voting.Queries.GetAllVotes;
using Microsoft.AspNetCore.Mvc;
using Voter.Controllers.Base;

namespace Voter.Controllers;

public class VotesController : ApiControllerBase
{
    [HttpPost("AddVote")]
    public async Task<IActionResult> AddVote([FromBody] AddVoteCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpGet("GetAllVotes")]
    public async Task<IActionResult> GetAllVotes()
    {
        var result = await Mediator.Send(new GetAllVotesQuery());
        return Ok(result);
    }
}
