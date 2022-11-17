using ApiValidationSample.Services;
using ApiValidationSample.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the FluentValidation
builder.Services.AddFluentValidationAutoValidation();
// Register from the assembly.
builder.Services.AddValidatorsFromAssemblyContaining<DogValidator>();
// Or register a single validator
//builder.Services.AddScoped<IValidator<Dog>, DogValidator>();

// Registering IDogToyService
builder.Services.AddScoped<IDogToyService, DogToyService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
