using GenericRepositorySample.Data;
using GenericRepositorySample.Entities;
using GenericRepositorySample.Repositories;
using GenericRepositorySample.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Services for dependency injection.
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("GenericSampleDatabase"));
builder.Services.AddTransient<IGenericRepository<Student>, GenericRepository<Student>>();
builder.Services.AddTransient<IClassRoomRepository, ClassRoomRepository>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
