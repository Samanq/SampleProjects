using MediatRAndCQRSSample.Application;
using MediatRAndCQRSSample.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddInfrastructure() // Registering Infrastrucre services
    .AddApplication();  // Registering application services

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
