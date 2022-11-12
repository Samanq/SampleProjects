using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

var builder = WebApplication.CreateBuilder(args);

//ChromeOptions _chromeOptions = new ChromeOptions();
//_chromeOptions.AddArgument("--incognito");
//_chromeOptions.AddArgument("--disable-gpu");
//_chromeOptions.AcceptInsecureCertificates = true;
//builder.Services.AddScoped<IWebDriver>(w => new ChromeDriver(_chromeOptions));

builder.Services.AddScoped<IWebDriver, ChromeDriver>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
