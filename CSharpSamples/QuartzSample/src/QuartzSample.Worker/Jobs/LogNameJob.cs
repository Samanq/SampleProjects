using Quartz;

namespace QuartzSample.Worker.Jobs;

[DisallowConcurrentExecution]
public class LogNameJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        string date = $"Name: Saman\r\n";
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "name.txt");

        using (StreamWriter streamWriter = new StreamWriter(path,true))
        {
            streamWriter.WriteLine(date);
        }

        return Task.CompletedTask;
    }
}
