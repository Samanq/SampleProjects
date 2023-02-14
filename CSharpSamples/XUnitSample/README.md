# XUnit Sample
xUnit.net is a free, open source, community-focused unit testing tool for the .NET Framework.<br>

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

## Asserts list
| Purpose | Command| Usage |
|---|---|---|
| Is True | True(value) | Assert.True(sut.IsActive); |
| Is False | False(value) | Assert.False(sut.IsActive); |
| Check Value | Equal(expectedValue, actualValue) | Assert.Equal("John Doe", sut.FullName); |
| Check Value | NotEqual(expectedValue, actualValue) | Assert.Equal("John Doe", sut.FullName); |
| Check Value with precision | Equal(expectedValue, actualValue, precision) | Assert.Equal(1.667, sut.DivideTwoNumbers, 3); |
| Check string's beginning | StartsWith(expectedValue, actualValue) | Assert.Equal("John", sut.FullName); |
| Check string's end | EndsWith(expectedValue, actualValue) | Assert.Equal("Doe", sut.FullName); |
| Check string's value and ignore case sensitivity | EndsWith(expectedValue, actualValue, ignoreCase: true ) | Assert.Equal("Doe", sut.FullName, ignoreCase: true); |
| Check string's value contains charecters | Contains(charecters, value) | Assert.Contains("jh oe", sut.FullName); |
| Check does not contains | Contains(charecters, value) | Assert.Contains("jh oe", sut.FullName); |
| Check with regular expresion | Matches(regularExpression, Actual Value) | Assert.Matches("[A-Z]", sut.FullName); |
| Check Range | InRange\<type>(ActualValue, min, hight) | Assert.InRange\<int>(sut.code, 100, 200); |
| Check Null | Null(Value) | Assert.Null(sut.Children); |
| Check Not Null | NotNull(Value) | Assert.NotNull(sut.Children); |
| Check all items in a collection | All(ActualValue, condition) | Assert.All(sut.Children, child => Assert.False(string.IsNullOrWhiteSpace(child))); |
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