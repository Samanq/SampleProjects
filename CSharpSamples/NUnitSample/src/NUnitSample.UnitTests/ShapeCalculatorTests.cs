namespace NUnitSample.UnitTests;

using NUnitSample.Application.Interfaces;
using NUnitSample.Application.Shapes;

public class ShapeCalculatorTests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void AggregateAreas()
    {
        // Arrange (Initiate our objects)
        var shapeCalculator = new ShapeCalculator();

        var shapes = new List<IShape>
        {
            new Rectangle(10,8),
            new Rectangle(12.5,7.5),
            new Circle(15),
            new Circle(4.5)
        };

        // Act (Call the method we want to test)
        var result = shapeCalculator.AggregateAreas(shapes);

        // Assert (Compare the result the expected values)
        Assert.That(result, Is.EqualTo(944.2256).Within(0.005));
    }
}