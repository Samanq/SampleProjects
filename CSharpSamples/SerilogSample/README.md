# Serilog

## Required Packages
Install these packages
- Serilog.AspNetCore
- Serilog.Enrichers.Environment (If you need to get machine name)
- Serilog.Enrichers.Thread (If you need to get Thread)
- Serilog.Enrichers.Process (If you need to get Process)
- Serilog.Sinks.Seq (If you need to use seq)
---
## Instructions
1. Remove Logging section in appsetings.json
2. Add serilog configuration in appsettings.json
```json
  "Serilog": {
    "Using": [],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning", // Only log warings from Microsoft namespace
        "System": "Warning" // Only log warings from System namespace
      }
    },
    "Enrich": [ "FromLogContext", "WithMachineName", "WithProcessId", "WithThreadId" ], // Add these data to the logs (you should install Enrichers packages before)
    "WriteTo": [
      { "Name": "Console" }, // For logging in console
      {
        "Name": "File", // For logging in a txt file
        "Args": {
          "Path": "D:\\Demos\\Logs\\log.txt",
          "outputTemplate": "{Timestamp:G} {Message}{NewLine:1}{Exception:1}"
        }
      },
      {
        "Name": "File", // log in json
        "Args": {
          "path": "D:\\Demos\\Logs\\log.json",
          "formatter": "Serilog.Formatting.Json.JsonFormatter, Serilog"
        }
      },
      {
        "Name": "Seq", // log in Seq (You should install Seq before)
        "Args": {
          "serverUrl": "http://localhost:8887"
        }
      }
    ]
  }
```
3. Create a new LoggerConfiguration in program.cs
```C#
// Creat Serilog configuration
var loggerConfig = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Reading configuration from appsetting.json
    .CreateLogger();
```
4. Set the other configurations
```C#
// For logging in program.cs
Log.Logger = loggerConfig;

builder.Logging.ClearProviders();       // Clear all existing log providers
builder.Host.UseSerilog(loggerConfig); // Register Serilog
```
5. Use RequestLogging for logging every requests
```C#
app.UseSerilogRequestLogging(); // Log every request by serilog. (you should register builder.Host.UseSerilog(configuration) to work)
```
6. For Logging in console you can use Log class
```C#
Log.Information("Starting the WebApi...");
```
7. Inject the logger into your controller so you can use it
```C#
[Route("[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly ILogger<StudentsController> _logger;

    // Injecting Logger
    public StudentsController(ILogger<StudentsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GenerateError")]
    public IActionResult GenerateError()
    {
        for (int i = 0; i < 10; i++)
        {
            if (i == 8)
            {
                // Log Error
                _logger.LogError("{counter} is not valid", i);
            }
            else
            {
                // Log Information
                _logger.LogInformation("{counter} is approved", i);
            }
        }
        return Ok();
    }
}
```
---
## Runnig Seq in docker
```poweshell
docker run -d --restart unless-stopped --name seq -e ACCEPT_EULA=Y -v D:\Demos\Logs:/data -p 8887:80 datalust/seq:latest
```
After runnig the Seq you can browse it from http://localhost:8887