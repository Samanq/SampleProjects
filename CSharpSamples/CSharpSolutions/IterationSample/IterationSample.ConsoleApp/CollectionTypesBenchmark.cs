using BenchmarkDotNet.Attributes;

namespace IterationSample.ConsoleApp;

[MemoryDiagnoser]
public class CollectionTypesBenchmark
{
    [Params(10, 1000, 100_000)]
    public int Count { get; set; }

    private int[] numbersArray;

    private List<int> numbersList = new();


    [GlobalSetup]
    public void Setup()
    {
        for (int i = 1; i < Count; i++)
        {
            numbersList.Add(i);
        }

        numbersArray = numbersList.ToArray();
    }

    [Benchmark]
    public void ForArray()
    {
        for (int i = 0; i < numbersArray.Length; i++)
        {
            int s = numbersArray[i];
        }
    }

    [Benchmark]
    public void ForList()
    {
        for (int i = 0; i < numbersList.Count; i++)
        {
            int s = numbersArray[i];
        }
    }

    [Benchmark]
    public void ForSpan()
    {
        Span<int> numbersSpan = numbersArray.AsSpan();

        for (int i = 0; i < numbersSpan.Length; i++)
        {
            int s = numbersSpan[i];
        }
    }
}
