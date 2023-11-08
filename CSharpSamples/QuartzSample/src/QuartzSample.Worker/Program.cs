using Quartz;
using QuartzSample.Worker;
using QuartzSample.Worker.Jobs;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
        });
        services.AddQuartzHostedService(opt =>
        {
            opt.WaitForJobsToComplete = true;
        });
        services.AddHostedService<Worker>();
    })
    .Build();

var schedulerFactory = host.Services.GetRequiredService<ISchedulerFactory>();
var scheduler = await schedulerFactory.GetScheduler();

// Define the job and tie it to our LogDateJob class
IJobDetail job = JobBuilder.Create<LogDateJob>()
    .WithIdentity(nameof(LogDateJob), "SampleGroup")
    .Build();

// Creating a trigger with interval
ITrigger trigger = TriggerBuilder.Create()
    .WithIdentity("DefaultTrigger", "SampleGroup")
    .StartNow()
    .WithSimpleSchedule(x => x
        .WithIntervalInSeconds(10)
    .RepeatForever())
.Build();

// Scheduling the job
await scheduler.ScheduleJob(job, trigger);


IJobDetail nameJob = JobBuilder.Create<LogNameJob>()
    .WithIdentity(nameof(LogNameJob), "SampleGroup")
    .Build();

// Build a trigger for a specific moment in time, with no repeats:
ISimpleTrigger nameTrigger = (ISimpleTrigger)TriggerBuilder.Create()
    .WithIdentity("NameTrigger", "SampleGroup")
    .StartAt(new DateTimeOffset(2023, 11, 03, 13, 32, 0, TimeSpan.FromHours(1)))
    .ForJob(nameof(LogNameJob), "SampleGroup")
    .Build();


await scheduler.ScheduleJob(nameJob, nameTrigger);

await host.RunAsync();
