using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;

namespace SpanSample.ConsoleApp;

[MemoryDiagnoser]
public class FirstBenchmark
{
    readonly List<int> numberList = Enumerable.Range(0, 100_000).ToList();

    [Benchmark]
    public void IterateList()
    {
        foreach (int number in numberList) 
        {
            int result = number + 1;
        }
    }

    [Benchmark]
    public void IterateSpan()
    {
        Span<int> numbersSpan = CollectionsMarshal.AsSpan(numberList);

        foreach (int number in numbersSpan) 
        {
            int result = number + 1;
        }
    }
}
