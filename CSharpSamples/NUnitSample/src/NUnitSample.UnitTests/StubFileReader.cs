namespace NUnitSample.UnitTests;
using NUnitSample.Application.Interfaces;

public class StubFileReader : IFileReader
{
    public string ReadText(string path)
    {
        return "This is a";
    }
}

