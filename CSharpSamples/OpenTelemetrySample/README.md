# OpenTelemetry in .NET 8 Sample

## Implement Tracing
Install these Nuget packages on your WebApi Project
```powershell
dotnet add package OpenTelemetry.Extensions.Hosting
dotnet add package OpenTelemetry.Instrumentation.AspNetCore
dotnet add package OpenTelemetry.Instrumentation.Http
dotnet add package OpenTelemetry.Exporter.Console
```

Configure OpenTelemetry in Program.cs
```c#
// Define the service name for tracing
var serviceName = "OpenTelemetrySampleWebApiService";

// Add OpenTelemetry to the service container
builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
            .AddAspNetCoreInstrumentation()  // Instrument incoming HTTP requests
            .AddHttpClientInstrumentation()  // Instrument outgoing HTTP requests
            .AddConsoleExporter();           // Export traces to the console
    });
```

Now run the project and execute the WeatherForecast Get method. You will get a result like this in the console
```yaml
Activity.TraceId:            c90d430278842fa79fbf4e9d9f63f120
Activity.SpanId:             7050961d810f36a1
Activity.TraceFlags:         Recorded
Activity.ActivitySourceName: Microsoft.AspNetCore
Activity.DisplayName:        GET WeatherForecast
Activity.Kind:               Server
Activity.StartTime:          2024-09-29T09:58:49.6919052Z
Activity.Duration:           00:00:00.0350113
Activity.Tags:
    server.address: localhost
    server.port: 7260
    http.request.method: GET
    url.scheme: https
    url.path: /WeatherForecast
    network.protocol.version: 2
    user_agent.original: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/129.0.0.0 Safari/537.36
    http.route: WeatherForecast
    http.response.status_code: 200
Resource associated with Activity:
    service.name: OpenTelemetrySampleWebApiService
    service.instance.id: f7c865a2-0375-4809-aac8-887e14e7155a
    telemetry.sdk.name: opentelemetry
    telemetry.sdk.language: dotnet
    telemetry.sdk.version: 1.9.0
```

## Implement Metrics
Install these NuGet packages
```powershell
dotnet add package OpenTelemetry.Metrics
dotnet add package OpenTelemetry.Logs
```

Configure the metrics in program.cs
```c#
// Define the service name for tracing, metrics, and logs
var serviceName = "OpenTelemetrySampleWebApiService";

// Add OpenTelemetry to the service container
builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
            .AddAspNetCoreInstrumentation()  // Instrument incoming HTTP requests (tracing)
            .AddHttpClientInstrumentation()  // Instrument outgoing HTTP requests (tracing)
            .AddConsoleExporter();           // Export traces to the console
    })
    .WithMetrics(metricProviderBuilder =>    // New section for metrics.
    {
        metricProviderBuilder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
            .AddAspNetCoreInstrumentation()  // Metrics for incoming HTTP requests
            .AddRuntimeInstrumentation()     // Metrics for runtime (e.g., GC, CPU)
            .AddHttpClientInstrumentation()  // Metrics for outgoing HTTP requests
            .AddConsoleExporter();           // Export metrics to the console
    });
```