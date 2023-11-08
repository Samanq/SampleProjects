using Quartz;

namespace QuartzSample.Worker.Jobs;

[DisallowConcurrentExecution]
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
