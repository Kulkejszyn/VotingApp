using System.Net;
using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class ForbiddenException : ApplicationActionException
{
    public ForbiddenException(string message) : base(message)
    {
        StatusCode = HttpStatusCode.Forbidden;
    }
}
