using BenchmarkDotNet.Attributes;

namespace BenchmarkDotnetSample.ConsoleApp;

[MemoryDiagnoser]
public class SentenceGeneratorBenchmark
{
    private static readonly SentenceGenerator _sentenceGenerator = new();

    [Benchmark(Baseline = true)]
    public string NormalGenerate()
    {
        return _sentenceGenerator.GenerateSentenceOriginal();
    }

    [Benchmark]
    public string NormalGenerateWithWhile()
    {
        return _sentenceGenerator.GenerateSentenceWhitWhile();
    }

    [Benchmark]
    public string GenerateWithStringBuilder()
    {
        return _sentenceGenerator.GenerateSentenceWithStringBuilder();
    }
}
