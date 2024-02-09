using BenchmarkDotNet.Attributes;

namespace SpanSample.ConsoleApp.Benchmarks;

[MemoryDiagnoser]
public class Case05Benchmark
{
    private string _code = "TST-102321";

    [Benchmark(Baseline = true)]
    public bool StartWith()
    {
        return _code.StartsWith("TST");
    }


    [Benchmark]
    public bool StartWithSpan()
    {

        Span<char> chars = stackalloc char[3];
        chars[0] = 'T';
        chars[1] = 'S';
        chars[2] = 'T';

        return _code.AsSpan(0, 3).StartsWith(chars);
    }

    [Benchmark]
    public bool StartWithSpanLoop()
    {
        Span<char> chars = stackalloc char[3];
        chars[0] = 'T';
        chars[1] = 'S';
        chars[2] = 'T';

        ReadOnlySpan<char> input = _code.AsSpan(0, 3);

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != chars[i])
            {
                return false;
            }
        }

        return true;
    }
}
