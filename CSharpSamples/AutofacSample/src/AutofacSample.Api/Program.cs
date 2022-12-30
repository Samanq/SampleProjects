using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutofacSample.Api.Autofac;

var builder = WebApplication.CreateBuilder(args);

// Adding the AutofacModule
builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        // Registering a single Service.
        //builder.RegisterType<EmailService>().As<IEmailService>();

        // Registering a module.
        builder.RegisterModule(new AutofacModule());
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


app.UseAuthorization();
app.MapControllers();
app.Run();
