using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Voter.Controllers.Base;

[ApiController]
[Route("[controller]")]
public class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}