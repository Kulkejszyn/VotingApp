using System.Net;
using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class NotFoundException : ApplicationActionException
{
    public NotFoundException(string message) : base(message)
    {
        StatusCode = HttpStatusCode.NotFound;
    }
}
