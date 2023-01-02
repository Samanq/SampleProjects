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
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();


await host.RunAsync();
