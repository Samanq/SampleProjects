using BenchmarkDotNet.Attributes;

namespace BenchmarkDotnetSample.ConsoleApp;

[MemoryDiagnoser]
public class NumberGeneratorBenchmarch
{
    [Params(100,1000,10_000)] // Multiple test cases
    public int Count { get; set; }

    private int randomNumber;


    [GlobalSetup] // Global Setup, act as a constructor
    public void Setup()
    {
        Random random = new Random();

        randomNumber = random.Next(10,100);
    }

    [Benchmark]
    public void For()
    {
        var result = new int[Count];

        for (int i = 0; i < result.Length; i++)
        {
            var number = i + randomNumber;
        }
    }

    [Benchmark]
    public void Foreach()
    {
        var result = new int[Count];

        foreach (var item in result)
        {
            var number = item + randomNumber;
        }
    }
}
