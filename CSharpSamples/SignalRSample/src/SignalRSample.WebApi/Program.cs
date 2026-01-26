using SignalRSample.WebApi.Endpoints;
using SignalRSample.WebApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Add SignalR
builder.Services.AddSignalR();

// Add CORS for Blazor WASM client
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClient", policy =>
    {
        policy.WithOrigins("http://localhost:5024", "https://localhost:7085")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("BlazorClient");

// Map SignalR Hub
app.MapHub<OrderHub>("/hubs/orders");

// Map API Endpoints
app.MapOrderEndpoints();

app.Run();