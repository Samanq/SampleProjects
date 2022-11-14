using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Creat Serilog configuration
var configuration = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    //.Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();   // Clear all existing log providers
builder.Logging.AddSerilog(configuration); // Register Serilog

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


try
{
    Log.Information("Starting the WebApi...");

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
    app.UseSerilogRequestLogging(); // Log every request by serilog.

}
catch (Exception ex)
{
    Log.Fatal(ex, "WebApi faild to start");
}
finally
{
    Log.CloseAndFlush();
    Log.Information("Finnaly");

}


