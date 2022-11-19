using RepositoryPattern.Application.Common.Interfaces.Persistence;
using RepositoryPattern.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);


// Registering PersonRepository
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

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
