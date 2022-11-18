using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()   // Registering Application Dependencies
    .AddInfraStructure(builder.Configuration);  // Registering Insfrastructure Dependencies

builder.Services.AddControllers();
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
