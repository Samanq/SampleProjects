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
        _logger.LogInformation($"{nameof(Worker)} stopping...");

        // Disposing the httClient at the end.
        // Remember this is not a good approach to httpClient and it is only for learning purposes.
        _httpClient.Dispose();

        _logger.LogInformation($"{nameof(Worker)} stopped...");
        return base.StopAsync(cancellationToken);
    }
}