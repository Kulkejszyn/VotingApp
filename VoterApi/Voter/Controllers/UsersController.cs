using Application.Features.Users.Queries.GetAllUsers;
using Microsoft.AspNetCore.Mvc;
using Voter.Controllers.Base;

namespace Voter.Controllers;

public class UsersController : ApiControllerBase
{
    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await Mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }
}
