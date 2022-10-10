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
builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
```

## Via error endpoint

## Custom Problem Details Factory
