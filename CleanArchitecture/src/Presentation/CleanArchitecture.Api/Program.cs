using CleanArchitecture.Api;
using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()  // Registering Presentation Dependencies
    .AddApplication()   // Registering Application Dependencies
    .AddInfraStructure(builder.Configuration);  // Registering Insfrastructure Dependencies

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.UseAuthorization();
app.MapControllers();

app.Run();
