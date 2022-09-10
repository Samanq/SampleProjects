using NUnitSample.Application.Animals;
using NUnitSample.Application.Interfaces;

namespace NUnitSample.UnitTests;

[TestFixture]
public class AnimalFactoryTests
{
    private AnimalFactory _animalFactory;

    [SetUp]
    public void Setup()
    {
        _animalFactory = new AnimalFactory();
    }

    [Test]
    public void CreateAnimal_flyableIsTrue_ReturnBird()
    {
        var result = _animalFactory.CreateAnimal(true);

        // Check the result is IAnimal or its derivatives 
        Assert.That(result, Is.InstanceOf<IAnimal>());

        // Check the type of result.
        Assert.That(result, Is.TypeOf<Bird>());
    }

    [Test]
    public void CreateAnimal_flyableIsFalse_ReturnDog()
    {
        var result = _animalFactory.CreateAnimal(false);

        // Check type of result.
        Assert.That(result, Is.TypeOf<Dog>());
    }
}
