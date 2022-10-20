# Global Error Handling

## Via Middleware
1. Create a folder named Middleware,
2. Create a class named ErrorHandlingMiddleware.
```C#
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;  // 500 if unexpected
        var result = JsonConvert.SerializeObject(new { error = ex.Message });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}
```
3. In program.cs add the custom middleware we created above.
```C#
 // Custom middleware for Error handling
app.UseMiddleware<ErrorHandlingMiddleware>();
```
---
## Via exception filter attribute
1. Create a folder named Filters
2. Create a class named ErrorHandlingFilterAttribute.cs that inherite from ExceptionFilterAttribute
3. Override OnException
```c#
public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        Exception exception = context.Exception;

        // context.Result = new ObjectResult(new { error = exception.Message })
        // {
        //     StatusCode = 500,
        // };

        // Using ProblemDetails instead of custom message
        var problemDetails = new ProblemDetails
        {
            Title = exception.Message,
            Status = (int)HttpStatusCode.InternalServerError,
        };
        context.Result = new ObjectResult(problemDetails);

        context.ExceptionHandled = true;
    }
}
```
4. Now you can add this attribute to every class you need.
```C#
[Route("api/[controller]")]
[ApiController]
[ErrorHandlingFilter]
public class StudentsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(students);
    }

    [HttpPost]
    public IActionResult Create(Student student)
    {
        var currentStudent = students.SingleOrDefault(s => s.Email.ToLower() == student.Email.ToLower());

        if (currentStudent is not null)
        {
            throw new Exception("Email already exist");
        }

        students.Add(student);

        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }
}
```
5. Or add to all controllers via options into program.cs
```C#
// Add ErrorHandlingFilterAttribute to all Controllers
builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
```

## Via Middleware and error endpoint
1. In Program.cs add UseExceptionHandler(route)
```C#
app.UseExceptionHandler("/error");
```
2. Create a Controller named ErrosController
```C#
namespace MiddlewareAndErrorEndpoint.Controllers;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)] // We add this because if we define an action without http method attribute, swagger will not run properly
public class ErrorsController : ControllerBase
{
    // No Http Method
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem(title: exception.Message);
    }
}

```
## Endpoint with custom ProblemDetailsFactory
1. Cerate a folder named Errors.
2. Create a class and inherite from ProblemDetailsFactoty and modify it as you like.
```C#
public class MyCustomProblemDetailsFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;

    public MyCustomProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        statusCode ??= 500;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        if (modelStateDictionary == null)
        {
            throw new ArgumentNullException(nameof(modelStateDictionary));
        }

        statusCode ??= 400;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        if (title != null)
        {
            // For validation problem details, don't overwrite the default title with null.
            problemDetails.Title = title;
        }

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;

        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId != null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }

        // Add our custom property
        problemDetails.Extensions.Add("customProperty", "custumValue");
    }
}
```
3. Add the CustomProblemDetailsFactory as a service to Programs.cs
```C#
// Add Our CustomProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory, MyCustomProblemDetailsFactory>();
```

4. In Program.cs add UseExceptionHandler(route)
```C#
app.UseExceptionHandler("/error");
```