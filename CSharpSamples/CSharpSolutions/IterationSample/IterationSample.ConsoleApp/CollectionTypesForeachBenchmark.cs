using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;

namespace IterationSample.ConsoleApp;

[MemoryDiagnoser]
public class CollectionTypesForeachBenchmark
{
    [Params(10, 1000, 100_000)]
    public int Count { get; set; }

    private List<int> numbersList = new();


    [GlobalSetup]
    public void Setup()
    {
        for (int i = 1; i < Count; i++)
        {
            numbersList.Add(i);
        }
    }

    [Benchmark]
    public void ForList()
    {
        foreach (int item in numbersList)
        {
            var s = item;
        }
    }

    [Benchmark]
    public void ForArray()
    {
        var array = numbersList.ToArray();

        foreach (int item in array)
        {
            var s = item;
        }
    }

    [Benchmark]
    public void ForSpan()
    {
        Span<int> numbersSpan = CollectionsMarshal.AsSpan(numbersList);

        foreach (int item in numbersList)
        {
            var s = item;
        }
    }


}
