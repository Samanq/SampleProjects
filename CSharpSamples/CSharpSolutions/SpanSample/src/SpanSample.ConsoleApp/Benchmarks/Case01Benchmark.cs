using BenchmarkDotNet.Attributes;
using SpanSample.ConsoleApp.Helpers;

namespace SpanSample.ConsoleApp.Benchmarks;

[MemoryDiagnoser]
public class Case01Benchmark
{
    private readonly string _input = "hello, world";

    [Benchmark(Baseline = true)]
    public void Substring()
    {

        string world = _input
            .Substring(startIndex: 7, length: 5); // With heap allocation

        Console.WriteLine(world);
    }

    [Benchmark]
    public void SpanSlice()
    {
        ReadOnlySpan<char> world = _input
            .AsSpan()
            .Slice(start: 7, length: 5); // Without heap allocation

        ConsoleX.WriteLine(world);
    }
}
