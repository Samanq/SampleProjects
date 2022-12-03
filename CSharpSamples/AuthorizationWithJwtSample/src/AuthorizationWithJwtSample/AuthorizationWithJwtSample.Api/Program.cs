using AuthorizationWithJwtSample.Application;
using AuthorizationWithJwtSample.Infrastructure;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddApplication()                           // Registering Application dependencies
    .AddInfrastructure(builder.Configuration);  // Regestering Infrastructure dependencies and passing the configuration

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add this option to enable authentication for Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorizationm heade using the bearer scheme (bearer {token})",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();    // Adding Authentication
app.UseAuthorization();     // Adding Authorization.
app.MapControllers();
app.Run();
