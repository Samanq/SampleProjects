namespace NUnitSample.UnitTests;
using NUnitSample.Application;

[TestFixture]
public class TextServiceTests
{
    private TextService _textService;

    [SetUp]
    public void Setup()
    {
        _textService = new TextService(new StubFileReader());
    }

    [Test]
    public void CreateBoldTag_WhenCalled_ShouldEncloseWithStrong()
    {
        var keyword = "abcd";
        var result = _textService.CreateBoldTag(keyword);

        Assert.That(result, Does.StartWith("<strong>"));
        Assert.That(result, Does.EndWith("</strong>"));

        // Check if the result contains the keywords.
        // IgnoreCase will ignore the case sensitivity of the keyword.
        Assert.That(result, Does.Contain(keyword).IgnoreCase);
    }

    [Test]
    public void ReadTextFromFile_WhenCalled_ReturnContent()
    {
        var result = _textService.ReadTextFromFile();

        Assert.That(result, Does.StartWith("This is a").IgnoreCase);
    }
}
