using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Define the service name for tracing
var serviceName = "OpenTelemetrySampleWebApiService";

builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
        .AddAspNetCoreInstrumentation()  // Instrument incoming HTTP requests
        .AddHttpClientInstrumentation()  // Instrument outgoing HTTP requests
        .AddConsoleExporter();           // Export traces to the console
    })
    .WithMetrics(metricProviderBuilder =>
    {
        metricProviderBuilder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
            .AddAspNetCoreInstrumentation()  // Metrics for incoming HTTP requests
            .AddHttpClientInstrumentation()  // Metrics for outgoing HTTP requests
            .AddRuntimeInstrumentation()     // Metrics for runtime (GC, memory, etc.)
            .AddConsoleExporter();           // Export metrics to the console
    });

// Configure logging with OpenTelemetry
builder.Logging.ClearProviders(); // Clear default logging providers
builder.Logging.AddOpenTelemetry(options =>
{
    options.IncludeFormattedMessage = true;
    options.IncludeScopes = true;
    options.ParseStateValues = true;

    // Optional: Add a console exporter for logs
    options.AddConsoleExporter();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
