using BenchmarkDotNet.Attributes;
using SpanSample.ConsoleApp.Helpers;

namespace SpanSample.ConsoleApp.Benchmarks;

[MemoryDiagnoser]
public class Case06Benchmark
{
    Guid _id = Guid.NewGuid();
    string _friendlyGuid = "rA-5vq1hvkWnal4W_kGrGQ";

    [Benchmark]
    public string GuidToFriendlyString()
    {
        return GuidHelper.GuidToFriendlyString(_id);
    }

    [Benchmark]
    public string GuidToFriendlyStringWithSpan()
    {
        return GuidHelper.GuidToFriendlyStringWithSpan(_id);
    }

    [Benchmark]
    public Guid FriendlyStringToGuid()
    {
        return GuidHelper.FriendlyStringToGuid(_friendlyGuid);
    }

    [Benchmark]
    public Guid FriendlyStringToGuidWithSpan()
    {
        return GuidHelper.FriendlyStringToGuidWithSpan(_friendlyGuid);
    }
}
