using BenchmarkDotNet.Attributes;
using System.Reflection;
using System.Text;

namespace SpanSample.ConsoleApp.Benchmarks;

[MemoryDiagnoser]
public class Case06Benchmark
{
    private readonly string _path = Path
        .Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\LargeFile.txt");

    [Benchmark(Baseline = true)]
    public string ReadFileWithString()
    {
        return File.ReadAllText(_path);
    }
    
    [Benchmark]
    public string ReadFileWithSpan()
    {
        byte[] fileBytes = File.ReadAllBytes(_path);
        return Encoding.UTF8.GetString(fileBytes);
    }
}
