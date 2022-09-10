namespace NUnitSample.Application;
using NUnitSample.Application.Interfaces;

public class FileReader : IFileReader
{
    public string ReadText(string path)
    {
        return File.ReadAllText(path);
    }
}
