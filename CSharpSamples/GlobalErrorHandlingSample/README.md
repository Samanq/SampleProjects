# Glonal Error Handling

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

## Via exception filter attribute

## Problem Details

## Via error endpoint

## Custom Problem Details Factory
