using System.Net;

namespace Domain.Exceptions.Base;

public abstract class ApplicationActionException : Exception
{
    protected ApplicationActionException(string message) : base(message)
    {
    }

    public HttpStatusCode StatusCode { get; protected init; }
}
