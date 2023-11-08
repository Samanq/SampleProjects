# Quartz Sample
Quartz.NET is a full-featured, open source **job scheduling system** that can be used from smallest apps to large scale enterprise systems.

## Dependencies
Install **Quartz** and  **Quartz.Extensions.DependencyInjection** Packages.<br>
We can also install **Quartz.Extensions.Hosting** instead.

## Jobs
**Job** represents a unit of work or a task that needs to be executed on a specific schedule. It defines the work that needs to be performed and can be associated with a **trigger**, which specifies when and how often the job should run.

1. Create a folder named **Jobs** and create a class inside the jobs folder and implement the **IJob** interface. Inside the **Execute** method, we can define the job behavior.

```C#
public class LogDateJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        string date = $"Date: {DateTime.Now}\r\n";
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");

        using (StreamWriter streamWriter = new StreamWriter(path,true))
        {
            streamWriter.WriteLine(date);
        }

        return Task.CompletedTask;
    }
}
```

## Configuring and running a Job
Then we should configure the services and create a job.<br>
We are doing it inside the Program.cs
```C#
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

await host.RunAsync();
```

## Triggers
Triggers define when and how often the job should run. There are various types of triggers available, but one of the most common is the SimpleTrigger, which schedules a job to run at specific intervals.<br>
Jobs can be assigned to multiple triggers.

There are different types of triggers
- WithCalendarIntervalSchedule
- WithCronSchedule
- WithDailyTimeIntervalSchedule
- WithSimpleSchedule
    ```C#
    ITrigger trigger = TriggerBuilder.Create()
    .WithIdentity("DefaultTrigger", "SampleGroup")
    .StartNow()
    .WithSimpleSchedule(x => x
        .WithIntervalInSeconds(10)
    .RepeatForever())
    .Build();

    ```
