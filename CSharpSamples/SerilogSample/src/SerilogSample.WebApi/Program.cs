using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Creat Serilog configuration
var loggerConfig = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Reading configuration from appsetting.json
    .CreateLogger();

// For logging in program.cs
Log.Logger = loggerConfig;

builder.Logging.ClearProviders();       // Clear all existing log providers
builder.Host.UseSerilog(loggerConfig);  // Register Serilog
//builder.Logging.AddSerilog(configuration); // Register Serilog 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(); // Log every request by serilog. (you should register builder.Host.UseSerilog(configuration) to work)
app.UseAuthorization();
app.MapControllers();

try
{
    // Log Information
    Log.Information("Starting the WebApi...");
    app.Run();
}
catch (Exception ex)
{
    // Log Fatal
    Log.Fatal(ex, "WebApi faild to start");
}
finally
{
    Log.CloseAndFlush();
}
