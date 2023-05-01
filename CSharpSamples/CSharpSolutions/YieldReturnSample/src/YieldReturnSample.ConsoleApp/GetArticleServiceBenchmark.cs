using BenchmarkDotNet.Attributes;

namespace YieldReturnSample.ConsoleApp;

[Config(typeof(AntiVirusFriendlyConfig))]
[MemoryDiagnoser]
public class GetArticleServiceBenchmark
{
    private readonly StudentService studentService = new();


    [Benchmark]
    public void GetNormally()
    {
        var result = studentService.GetArticleLinesNormally();
        
        foreach (var line in result) 
        {
            Console.WriteLine(line);
        }
    }

    [Benchmark]
    public async Task GetYieldAsync()
    {
        var result = studentService.GetArticleLinesYield();

        await foreach (var line in result)
        {
            Console.WriteLine(line);
        }
    }
}
