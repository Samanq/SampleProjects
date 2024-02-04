using BenchmarkDotNet.Attributes;

namespace SpanSample.ConsoleApp.Benchmarks;

[MemoryDiagnoser]
public class Case04Benchmark
{
    [Benchmark(Baseline = true)]
    public void ArrayInHeap()
    {
        int[] numbersInHeap = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

    }

    [Benchmark]
    public void ArrayInStack()
    {
        Span<int> numbersInStack = stackalloc int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    }
}
