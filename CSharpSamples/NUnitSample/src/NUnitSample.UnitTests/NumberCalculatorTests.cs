namespace NUnitSample.UnitTests;

using Microsoft.VisualBasic;
using NUnitSample.Application.Math;

[TestFixture]
public class NumberCalculatorTests
{
    private NumberCalculator _numberCalculator;

    [SetUp]
    public void Setup()
    {
        // This will run before every test.
        _numberCalculator = new NumberCalculator();
    }

    [TearDown]
    public void TearDown()
    {
        // This will run after every test.
    }

    [Test]
    public void AddTwoNumber_WhenCalled_ReturnSumOfArguments()
    {
        // Arrange (Initiate the objects we need to test.)
        var numberCalculator = new NumberCalculator();

        // Act (Call the method we want to test)
        var result = numberCalculator.AddTowNumber(1.5, 2.5);

        // Assert (Compare the result the expected values)
        Assert.That(result, Is.EqualTo(4));
    }

    [Test]
    // 5 is a, 2 is b, 3 is expected value.
    [TestCase(5, 2, 3)]
    // 5.5 is a, 2.5 is b, 3 is expected value.
    [TestCase(5.5, 2.5, 3)]
    // 3 is a, 5 is b, -2 is expected value.
    [TestCase(3, 5, -2)]
    public void SubtractTwoNumber_WhenCalled_ReturnDifferenceOfArguments(double a, double b, double expectedResult)
    {
        // Act
        var result = _numberCalculator.SubtractTowNumber(a, b);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void GetOddNumbers_WhenCalled_ReturnOddNumbers()
    {
        var result = _numberCalculator.GetOddNumbers(1, 5);

        // Check the has any result.
        Assert.That(result, Is.Not.Null);

        // Check if the result is equivalent to this array. (Order is not important!)
        Assert.That(result, Is.EquivalentTo(new int[] { 5, 1, 3 }));

        // Check if the result is sorted.
        Assert.That(result, Is.Ordered);
    }

    [Test]
    public void DistinctNumber_WhenCalled_ReturnUniqueNumber()
    {
        var result = _numberCalculator.DistinctNumbers(new int[] { 1, 5, 3, 8, 3, 4, 5, 4 });

        Assert.That(result, Is.Unique);
    }

    [Test]
    public void SortNumbers_WhenCalled_ReturnArrayInAscendingOrder()
    {
        var result = _numberCalculator.SortNumbersAscending(new int[] { 2, 3, 1 });

        Assert.That(result, Is.Ordered);
    }

    [Test]
    public void SortNumbers_WhenCalled_ReturnArrayInDescendingOrder()
    {
        var result = _numberCalculator.SortNumbersDescending(new int[] { 5, 6, 8 });

        Assert.That(result, Is.Ordered.Descending);
    }
}