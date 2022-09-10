using NUnitSample.Application.Animals;

namespace NUnitSample.UnitTests;
[TestFixture]
public class BirdTests
{
    [Test]
    public void Speak_WhenCalled_SetTheLastWordProperty()
    {
        // Arrange
        var bird = new Bird();
        var sentence = "Hello World";

        // Act
        bird.Speak(sentence);

        // Assert
        Assert.That(bird.LastWord, Is.EqualTo(sentence));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void Speak_BadArgument_ThrowAgrumentNullException(string sentence)
    {
        var bird = new Bird();

        Assert.That(() => bird.Speak(sentence), Throws.ArgumentNullException);
        // or
        //Assert.That(() => bird.Speak(sentence), Throws.Exception.TypeOf<ArgumentNullException>());
    }
}
