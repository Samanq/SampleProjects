using BenchmarkDotNet.Attributes;

namespace YieldReturnSample.ConsoleApp;

[MemoryDiagnoser]
public class GetStudentServiceBenchmark
{
    private readonly StudentService studentService = new();


    [Benchmark]
    public void GetNormally()
    {
         var result = studentService.GenerateStudentNormally(100);
    }

    [Benchmark]
    public void GetYield()
    {
        var result = studentService.GenerateStudentYield(100);
    }
}
