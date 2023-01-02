# Worker Service Sample

Worker service is a background service that is runs in background and it just keep running forever.

Workers can work on windows, linux and Azure.


---

## Creating the Project and installing packages.
1. Creating new **Worker Service** project.
2. Install these packages
    - **Serilog.AspNetCore** (For logging purposes)
    - **Serilog.Sinks.File** (For logging into files)
    - **Microsoft.Extensions.Hosting.WindowsServices** (For installing as a windows service)

## Setting-up the service.
1. Open the **Worker.cs** override these three methods and implement them.
    -  StartAsync (Only runs once when the service started)
    -  ExecuteAsync (Executes the main logic)
    -  StopAsync ( Only runs when the worker service shutdown)
```C#
namespace WorkerServiceSample.WorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private HttpClient _httpClient;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    // Only runs once when the service started.
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        // Initializing the httpClient
        _httpClient = new HttpClient();

        return base.StartAsync(cancellationToken);
    }


    // Executes the main logic
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Works as long as cancelllation requested.
        while (!stoppingToken.IsCancellationRequested)
        {
            // Sending a get request to this url to retrieve the website 
            var result = await _httpClient.GetAsync("http://netdiag.net");
            if (result.IsSuccessStatusCode) 
            {
                // Log the status code of the request if it's successful.
                _logger.LogInformation("Website is up and running with: {StatusCode} code", result.StatusCode);
            }
            else 
            {
                // Log an error if the website is not working.
                _logger.LogError("Website is not runnig with: {StatusCode} code", result.StatusCode);
            }
            
            // Waiting for 10 seconds.
            await Task.Delay(10000, stoppingToken);
        }
    }

    // Only runs when the worker service shutdown.
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        // Disposing the httClient at the end.
        // Remember this is not a good approach to httpClient and it is only for learning purposes.
        _httpClient.Dispose();

        return base.StopAsync(cancellationToken);
    }
}
```

## Impelenting the logging (using Serilog).
1. Open **Program.cs** and add serilog configuration
```C#
using Serilog;
using WorkerServiceSample.WorkerService;

// Configuring the Serilog settings
// This approach is not recommended, it is only for tutorial purposes.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(@"C:\WorkerServiceSample\LogFile.txt")
    .CreateLogger();

Log.Information("Worker Service is starting");


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog() // Add this line to use serilog
    .Build();


await host.RunAsync();
```

## Installing the service as a windows service
1. Open **Program.cs** and in the **CreateHostBuilder** method add **UseWindowsService()**
```C#
IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService() // For installing as a windows service
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog() // For using serilog
    .Build();
```
2. Right click on project and click on **publish** then, set and location and click finish.
3. In the publish window click Publish.
4. Open powershell as administrator and run this command
```powershell
sc.exe create WorkerServiceSample binpath= C:\Publish\Services\WorkerServiceSample\WorkerServiceSample.WorkerService.exe start= auto
```
5. Now if you open services.msc you can see the WorkerSampleService which is not running.
6. Right click on **WorkerSampleService** and click on Start.

## Uninstalling the windows service
1. Open powershell as administrator and run this command
```powershell
sc.exe delete WorkerServiceSample
```