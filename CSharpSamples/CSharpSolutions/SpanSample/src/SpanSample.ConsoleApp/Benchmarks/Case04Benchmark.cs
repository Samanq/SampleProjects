using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Runtime;
using System.Text;

namespace SpanSample.ConsoleApp.Benchmarks;

[MemoryDiagnoser]
public class Case04Benchmark
{
    private readonly string _input = "rem Ipsum is simply dummy text of the printing and typesetting, industry. Lorem Ipsum has been the industry's standard, dummy text ever since the 1500s, when an unknown, printer took a galley of type and, scrambled it to, make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised, in the 1960s, with the release of, Letraset sheets containing, Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";

    [Benchmark(Baseline = true)]
    public void Replace()
    {

        string result = _input.Replace(',', '-');
    }

    [Benchmark]
    public void NewSpan()
    {
        Span<char> chars = stackalloc char[_input.Length];

        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == ',')
            {
                chars[i] = '-';
            }
        }
    }
}
