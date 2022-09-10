namespace NUnitSample.Application;
using NUnitSample.Application.Interfaces;

public class TextService
{
    private readonly IFileReader _fileReader;

    public TextService(IFileReader? fileReader = null)
    {
        _fileReader = fileReader ?? new FileReader();
    }

    public string CreateBoldTag(string content)
    {
        return $"<strong>{content}</strong>";
    }

    public string ReadTextFromFile()
    {
        var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"../../../../../assets/sample.txt"));
        var result = _fileReader.ReadText(path);
        return result;
    }
}
