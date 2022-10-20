using CustomProblemDetailsFactory.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

// Add Our CustomProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory, MyCustomProblemDetailsFactory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.UseAuthorization();
app.MapControllers();
app.Run();
