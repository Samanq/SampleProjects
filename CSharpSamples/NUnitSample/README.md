# Project setup and Configuration
1. Add a new project (class library) and name it YourProjectName.UnitTests (In case of you want to write unit tests.)

2. Right click on Dependencies and Add a Refrence to the project that you want to test.

3. Install these packages from NuGet:
    * NUnit
	* NUnit3TestAdapter
	* Microsoft.Net.Test.Sdk
---

## Writing the first test
1. Add a public class and  with [TestFixture] attribute name it ClassNameShould ([TestFixture] it's not compulsory in NUnit3).

3. Add a public void method with [Test] attribute and name it \<AggregateAreas>.

4. Instantiate the class you want to test in name that variable sut (system under 
test) => Arrange Phase

5. Call the method you want to test and pass the arguments. => Act Phase

6. Use Assert command to compare the result => Assert Phase
 
7. Open Test Explorer and run test.

```c#
[Test]
public void SumTwoNumber_WhenCalled_ReturnSumOfArguments()
{
    // Arrange (Initiate our objects)
    var numberCalculator = new NumberCalculator();

    // Act (Call the method we want to test)
    var result = numberCalculator.SumTowNumber(1.5, 2.5);

    // Assert (Compare the result the expected values)
    Assert.That(result, Is.EqualTo(4));
}
```

## SetUp and TearDown
 We can define two methods called SetUp and TearDown that they 

 ```c#
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
 ```
 ---

 ## Test with diffrent data (Parameterized Tests)
 We test our method with diffrent data.
 Add [TestCase] attribute over the test method and set parameters, the last parameter is excpected value.

 ```c#
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
 ```

 ---

# Asserts list
> ## Is
| Purpose | Command| Usage |
|---|---|---|
|Equal to a value |Is.EqualTo(value)|Assert.That(result, Is.EqualTo(4));|
| Is true | Is.True() | Assert.That(result, Is.True()); |
| Is Null | Is.Null |  |
| Is Not Null | Is.Not.Null |  |
| Is Empty | Is.Empty |  |
| Is Not Empty | Is.Not.Empty |  |
| Is Greater than | Is.GreaterThan |  |
| Same object in memory | Is.SameAs(variable) |  |
| Not Same object in memory | Is.Not.SameAs(variable) |  |
| Equal Within Range |  | Is.EqualTo(0.33).Within(0.004); |
| Equal Within Range (With percent telorance) |  | Is.EqualTo(0.33).Within(10).Percent |
| List items are unique | Is.Unique | Assert.That(result, Is.Unique); |
| Array is sorted Ascending | Is.Ordered | Assert.That(result, Is.Ordered); |
| Array is sorted Descending | Is.Ordered.Descending | Assert.That(result, Is.Ordered.Descending); |
| Exact Type | Is.TypeOf\<value>() | Assert.That(result, Is.TypeOf\<Bird>()); |
| Exact type or derivatives  | Is.InstanceOf\<Value>() | Assert.That(result, Is.InstanceOf\<IAnimal>()); |

>## Has
| Purpose | Command| Usage |
|---|---|---|
| List items count | Has.Exactly(3).Items |  |
|  |  |  |
>## Does

| Purpose | Command| Usage |
|---|---|---|
| Start with | Does.StartWith(value) | Assert.That(result, Does.StartWith("\<strong>")); |
| End with | Does.EndWith(value) | Assert.That(result, Does.EndWith("\</strong>")); |
| Looking for espesific item in a list | Does.Contain(keyword) | Assert.That(result, Does.Contain("Keyword").IgnoreCase); |

>## Throws

| Purpose | Command| Usage |
|---|---|---|
---

## Testing a void method
In this test we test a value of a property inside the class we want to test. the method we test is void and doesn't return any value.

```C#
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
```
---
## Testing exception errors
In this test we check if our method throws exception error.

```C#
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
```

---
---

<!-- Has.Exactly(1)
	.Property("ProductName").Equal("a")
	.And
	.Property("InterestRate").Equal(1)				=> Locking for a property in a list
Has.Exactly(1)
	.Matches<ModelSameAsList>(
		item => item.ProductName == "a" &&
				item.InterestRate == 1)				=> Locking for a property in a list

Assert.That(() => new ClassName(0), Throws.TypeOf<ArgumentOutOfRangeException>());			=> Check constructor throw exception -->
---


```c#
// TestCase Sample2 (Simpilify)
[Test]
[TestCase(5, ExpectedResult = 50)]
[TestCase(6, ExpectedResult = 60)]
public int MultipyByTen(int number)
{
	var sut = new MyCalculator();
	
	return sut.MultiplyByTen(number);
}
```

---

# Attributes
| Attribute | Description |
|---|---|
| [Test] | Method is a test. |
| [TestCase()] | Allows method-level test data. |
| [TestCaseSource()] | Allows method-level test data with a source. |
| [Ignore("Message")] | Method will not run. |
| [Category("Title")] | Put in a specified category. |
| [Setup] | Method executes before every test. |
| [TearDown] | Method executes after every test. |
| [OneTimeSetup] | Method executes before the first test. |
| [OneTimeTearDown] | Method executes after the last test. |

