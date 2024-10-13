var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(); // Add the Service defaults

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("HEllo");
//});

app.MapDefaultEndpoints(); // Map the endpoint

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
