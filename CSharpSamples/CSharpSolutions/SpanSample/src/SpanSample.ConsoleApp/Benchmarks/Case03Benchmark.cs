using BenchmarkDotNet.Attributes;

namespace SpanSample.ConsoleApp.Benchmarks;

[MemoryDiagnoser]
public class Case03Benchmark
{
    private readonly int[] _originalNumberArray =
        Enumerable.Range(1, 100_000_000).ToArray();

    [Benchmark]
    public void NewArray()
    {
        int[] newNumberArray =
            _originalNumberArray;

        foreach (var item in newNumberArray)
        {
            var result = item;
        }
    }

    [Benchmark]
    public void NewSpan()
    {
        Span<int> newNumberSpan =
            _originalNumberArray.AsSpan();

        foreach (var item in newNumberSpan)
        {
            var result = item;
        }
    }
}
