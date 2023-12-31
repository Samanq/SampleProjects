using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;

namespace IterationSample.ConsoleApp;

[MemoryDiagnoser]
public class IterationBenchmark
{
    [Params(10, 100)]
    public int Count { get; set; }

    private List<Student> students = new List<Student>();

    [GlobalSetup]
    public void Setup()
    {
        Random random = new Random();

        for (int i = 1; i < Count; i++)
        {
            students.Add(new Student(Id: i, Name: $"John {i}", Age: random.Next(20, 80)));
        }
    }

    [Benchmark]
    public void For()
    {
        for (int i = 0; i < students.Count; i++)
        {
            var s = students[i].Name;
        }
    }

    [Benchmark]
    public void Foreach()
    {

        foreach (Student student in students)
        {
            var s = student.Name;
        }
    }

    [Benchmark]
    public void SpanFor()
    {
        Span<Student> result = CollectionsMarshal.AsSpan(students);

        for (int i = 0; i < result.Length; i++)
        {
            var s = result[i].Name;
        }
    }

    [Benchmark]
    public void SpanForeach()
    {
        Span<Student> result = CollectionsMarshal.AsSpan(students);

        foreach (Student student in result)
        {
            var s = student.Name;
        }
    }
}
