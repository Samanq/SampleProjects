# BenchmarkDotnet Sample
BenchmarkDotNet is a popular .NET library used for benchmarking code performance in C# and other .NET languages. It allows developers to measure the execution time, memory allocation, and other performance-related metrics of various pieces of code, such as methods, functions, or entire classes.

Github Repository: https://github.com/dotnet/BenchmarkDotNet

---

## Instalation
 Install **BenchmarkDotNet** package from NuGet.

---

 ## Writing a benchmark
1. Create a class and add the **[MemoryDiagnoser]** attribute.
2. Create your benchmark methods and use the **[Benchmark]** attribute for them. We can use the **Baseline = true** for the method that we want to get tested against others.
```C#
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
```

3. In Program.cs use Benchmarkrunner and run it for the benchmark class you've already written.
```C#
BenchmarkRunner.Run<SentenceGeneratorBenchmark>();
```
4. Build the project in **release** mode.
5. Run the project **without debugging**.
