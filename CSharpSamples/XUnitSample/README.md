# XUnit Sample and FluentAssertions
xUnit.net is a free, open source, community-focused unit testing tool for the .NET Framework.<br><br>
FluentAssertions is a very extensive set of extension methods that allow you to more naturally specify the expected outcome of a TDD or BDD-style unit tests.

---

## Method Naming Convention
There are some pattern to choose a name for the test methods but, we follow this convention.<br>
**MethodName_StateUnderTest_ExpectedBehavior** <br>
Sample: 
```C#
public void AddTwoNumbers_SmallerSecondNumber_ReturnsNegativeNumber
```
---

## Setup
1. Create a **ClassLibrary** project inside your solution and call it **SolutionName.Tests**
2. Install these Packages on the test project.
    - xunit
    - xunit.runner.visualstudio
    - Microsoft.NET.Test.Sdk
    - FluentAssertions
3. In the Test project add refrences to other projects you want to test.

## Writing First Test
---

## Constructor
We can use the constructor to instantiate some variable so we don't have to create a instance in every method. in this way constructor runs befor every test cases.
```C#
public class CalculatorServiceTest
{
    private readonly CalculatorService _sut;

    public CalculatorServiceTest()
    {
        // Runs before every task
        _sut = new CalculatorService();
    }

    [Fact]
    public void AddTwoNumbers_SimpleNumbers_ReturnsSum()
    {
        var actualValue = _sut.AddTwoNumbers(3, 2);
        Assert.Equal(5, actualValue);
    }
}
```
---

## Implementing IDisposable
We can Implement the IDisposable for out test class for disposing the value after the tests.
```C#
public class CalculatorServiceTest : IDisposable
{
    private readonly CalculatorService _sut;

    public CalculatorServiceTest()
    {
        _sut = new CalculatorService();
    }
    public void Dispose()
    {
        _sut.Dispose();
    }
}
```
---

## Sharing an instance between test methods and other classes.
1. First we should create a class and call it ClassNameFisxture and implement the **IDisposable**, the we can instantiate in the constructor and clean up in the Dispose method.
```C#
using XUnitSample.Infrastructure.Services;

namespace XUnitSample.Tests;

public class ShipFixture : IDisposable
{
    public Ship Ship { get; private set; }

    public ShipFixture()
    {
        Ship = new Ship();
    }

    public void Dispose()
    {
        // It will runs after the last test.
    }
}
```
2. Now create the test class and implement the IClassFixture<>
```C#
using Xunit;
using XUnitSample.Infrastructure.Services;

namespace XUnitSample.Tests;

public class ShipTest : IClassFixture<Ship>
{
    private readonly Ship _shipFixture;

	public ShipTest(Ship ship)
	{
		// Instantiate once in ShipFixture class
		_shipFixture = ship;
	}

	[Fact]
	public void Start_OnExecute_ChangeStateToStart()
	{
		_shipFixture.Start();
		Assert.Equal("Ship has started", _shipFixture.CurrentState);
	}

    [Fact]
    public void TurnOff_OnExecute_ChangeStateToTurnOff()
    {
        _shipFixture.TurnOff();
        Assert.Equal("Ship has turned off", _shipFixture.CurrentState);
    }
}
```
3. Now if you want to share this instance beetween classes you have to create a another class and call it ClassNameCollection and implement the ICollectionFixture<> you should also use [CollectionDefinition("Name collection")] attribute
```C#
using Xunit;

namespace XUnitSample.Tests;

[CollectionDefinition("Ship collection")]
public class ShipCollection : ICollectionFixture<ShipFixture>{}
```
4. Then if every class that you want to use this instance you should use [Collection("Name collection")] attribute.<br>
When we are using collection attribute we don't have to implement any class.
```C#
using Xunit;

namespace XUnitSample.Tests;

[Collection("Ship collection")]
public class ShipDeepTest
{
    private readonly ShipFixture _shipFixture;

    public ShipDeepTest(ShipFixture ship)
    {
        // Instantiate once in ShipFixture class
        _shipFixture = ship;
    }

    [Fact]
    public void Start_OnExecute_ChangeStateToStart()
    {
        _shipFixture.Ship.Start();
        Assert.Equal("Ship has started", _shipFixture.Ship.CurrentState);
    }

    [Fact]
    public void TurnOff_OnExecute_ChangeStateToTurnOff()
    {
        _shipFixture.Ship.TurnOff();
        Assert.Equal("Ship has turned off", _shipFixture.Ship.CurrentState);
    }
}
```
---

## Adding Custom output 
For having custom output message we have to inject **ITestOutputHelper** into our test class.
```C#
using Xunit;
using Xunit.Abstractions;
using XUnitSample.Infrastructure.Services;

namespace XUnitSample.Tests
{
    public class CalculatorServiceTest
    {
        private readonly ITestOutputHelper _output;

        public CalculatorServiceTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void AddTwoNumbers_SimpleNumbers_ReturnsSum()
        {
            var sut = new CalculatorService();

            // Using Custom output message.
            _output.WriteLine("Test started.");

            var actualValue = sut.AddTwoNumbers(3, 2);
            Assert.Equal(5, actualValue);
        }
    }
}
```
---

## Multiple Test cases with inline attribute
1. Add **Theory** Attribute for the test method.
2. Set data for testing in **InlineData** attribute.
3. Add the inputs in method parameter.
```C#
namespace XUnitSample.Tests
{
    public class CalculatorServiceTest
    {
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(2, 1, 3)]
        [InlineData(-1, -1, -2)]
        public void AddTwoNumbers_OnExecute_ReturnSum(int firstNumber, int secondNumber, int expectedResult)
        {
            // Arrange
            var sut = new CalculatorService();

            // Act
            var result = sut.AddTwoNumbers(firstNumber, secondNumber);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
```
---

## Sharing test cases beetween classes.
1. Create a class and call it InternalNameTestData.
2. Define the test cases as IEnumerable<object[]>.
```C#
namespace XUnitSample.Tests.TestData;

public class InternalNumbersTestData
{
    public static IEnumerable<object[]> SumTestData
    {
        get
        {
            yield return new object[] { 1, 2, 3 };
            yield return new object[] { 2, 1, 3 };
            yield return new object[] { -1, -1, -2 };
        }
    }
}
```
3. For the test methods we should use **Theory** and **MemberData** Attribute.
```C#
[Theory]
[MemberData(
    nameof(InternalNumbersTestData.SumTestData),
    MemberType = typeof(InternalNumbersTestData))]
public void AddTwoNumbers_OnExecute_ReturnSum(int firstNumber, int secondNumber, int expectedResult)
{
    // Arrange
    var sut = new CalculatorService();

    // Act
    var result = sut.AddTwoNumbers(firstNumber, secondNumber);

    // Assert
    Assert.Equal(expectedResult, result);
}
```
---

## Reading test cases from external file
1. Create a class and call it ExternalNumbersTestData.
```C#
namespace XUnitSample.Tests.ExternalTestData;

public class ExternalNumbersTestData
{
    public static IEnumerable<object[]> SumTestData
    {
        get
        {
            string[] csvLines = File.ReadAllLines(@"..\..\..\ExternalTestData\SumTestCases.csv");

            var testCases = new List<object[]>();

            foreach (var csvLine in csvLines)
            {
                IEnumerable<int> values = csvLine.Split(',').Select(int.Parse);

                object[] testCase = values.Cast<object>().ToArray();

                testCases.Add(testCase);
            }

            return testCases;
        }
    }
}
```
2. For the test methods we should use **Theory** and **MemberData** Attribute.
```C#
[Theory]
[MemberData(
    nameof(ExternalNumbersTestData.SumTestData),
    MemberType = typeof(ExternalNumbersTestData))]
public void AddTwoNumbers_OnExecuteWithExternalMemberData_ReturnSum(int firstNumber, int secondNumber, int expectedResult)
{
    // Arrange
    var sut = new CalculatorService();

    // Act
    var result = sut.AddTwoNumbers(firstNumber, secondNumber);

    // Assert
    Assert.Equal(expectedResult, result);
}
```
3. If you had problem with reading csv file path you can right click on the csv file and go to the properties and change the **Copy to Output Directory** to **Copy always**. <br>
or you can change the file path when you want to read the file.
---

## Creating custom data attribute
1. Create a class and call it NameDataAttribute and implement **DataAttribute**
```C#
public class NumbersSumDataAttribute : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return new object[] { 1, 2, 3 };
        yield return new object[] { 2, 1, 3 };
        yield return new object[] { -1, -1, -2 };
    }
}
```
2. Now you can use the new attribute For the test methods.
```C#
[Theory]
[NumbersSumData]
public void AddTwoNumbers_OnExecuteWithCustomDataAttribute_ReturnSum(int firstNumber, int secondNumber, int expectedResult)
{
    // Arrange
    var sut = new CalculatorService();

    // Act
    var result = sut.AddTwoNumbers(firstNumber, secondNumber);

    // Assert
    Assert.Equal(expectedResult, result);
}
```
---


## Asserts list
For Xunit we use Assert.<br>
For FluentAssertion we use ActualValue.<br>
av = Actual Value<br>
ev = Expected Value<br>

| Purpose | XUnit Syntax | FluentAssertions Syntax |
|---|---|---|
| Is True | .True(value) | .Should().Be(true); |
| Is False | .False(value) | .Should().Be(false); |
| Check Value | .Equal(ev, av) | .Should().Be(ev) |
| Check Value | .NotEqual(ev, av) | .Should().NotBe(ev); |
| Value with precision | .Equal(ev, av, precision) | .Should().BeApproximately(ev,precision) |
| Ignore case sensitivity | .Equal(ev, av, ignoreCase: true ) | .Should().BeEquivalentTo("ev"); |
| Check beginning  | .StartsWith(ev, av) | .Should().StartWith("ev") |
| Check ending | .EndsWith(ev, av) | .Should().EndWith("ev") |
| Contains | Contains(characters, ev) | .Should().Contain("ev") |
| Check does not contains | Contains(charecters, value) | Assert.Contains("jh oe", sut.FullName); |
| Match with wildcard | - | .Should().Match("*s") |
| Match with regular expression | Matches(regularExpression, Actual Value) | .Should().MatchRegex("[A-Z]") |
| Be one of | - | .Should().BeOneOf("ev", "ev") |
| Check Range | InRange\<type>(av, min, hight) | Assert.InRange\<int>(sut.code, 100, 200); |
| Check Null | Null(Value) | Assert.Null(sut.Children); |
| Check Not Null | NotNull(Value) | Assert.NotNull(sut.Children); |
| Check all items in a collection | All(av, condition) | Assert.All(sut.Children, child => Assert.False(string.IsNullOrWhiteSpace(child))); |
| Check Type | IsType\<Type>(value) | |
| Check Type | IsAssignableFrom\<Type>(value) | |
| Check Instance | NotSame(object1, object2) | |
| Check Instance | Same(object1, object2) | |
| Check exceptions | Throws\<Exception>(test code ) | |
| Check if an event raises | Raises\<TypeOfEventArgs>(attach, detach, action) | |
| - | PropertyChanaged\<TypeOfEventArgs>(object, NameOfTheProperty, action) | |

---

# Attributes
| Attribute | Description |
|---|---|
| [Fact] | Method is a test. |
| [Fact(Skip = "Reason")] | Skip a test. |
| [Trait("Category", "Name")] | Put in a specified category. |
| [Theory] | Runs multiple times with different data  |
| [InlineData()] | Providing multiple case data  |
| [MemberData()] | Get tes data from a class  |