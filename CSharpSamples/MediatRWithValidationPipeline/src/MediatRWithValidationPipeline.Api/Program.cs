using MediatRWithValidationPipeline.Application;
using MediatRWithValidationPipeline.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddApplication()       // Registering Application layer dependencies
    .AddInfrastructure();   // Registering Infrastructure layer dependencies

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
