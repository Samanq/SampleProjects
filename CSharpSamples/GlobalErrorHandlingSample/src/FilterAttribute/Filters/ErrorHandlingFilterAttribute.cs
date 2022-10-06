namespace FilterAttribute.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        Exception exception = context.Exception;

        context.Result = new ObjectResult(new { error = exception.Message })
        {
            StatusCode = 500,
        };

        context.ExceptionHandled = true;
    }
}
