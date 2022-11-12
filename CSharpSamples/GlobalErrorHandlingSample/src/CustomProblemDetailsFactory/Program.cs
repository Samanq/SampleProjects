using CustomProblemDetailsFactory.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

// Add Our CustomProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory, MyCustomProblemDetailsFactory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add ExceptionHandler with a route
app.UseExceptionHandler("/error");

// For Minimal implementation
//app.Map("/error", (HttpContext? httpContext) =>
//{
//    Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
//    return Results.Problem();
//});

app.UseAuthorization();
app.MapControllers();
app.Run();
