using Domain.Exceptions.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Voter;

public class ApplicationExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not ApplicationActionException exception) return;
        context.Result = new ObjectResult(exception.Message);
        context.HttpContext.Response.StatusCode = (int)exception.StatusCode;
        context.ExceptionHandled = true;
    }
}
