using Quartz;
using QuartzSample.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
        });
        services.AddQuartzHostedService();

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
