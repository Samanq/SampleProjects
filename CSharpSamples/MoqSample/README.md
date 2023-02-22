# MoqSample
Moq is a .NET framework. It is used for creating mock objects in unit testing, which are objects that simulate the behavior of real objects in order to isolate the code being tested and make it easier to test in a controlled environment.

---

## Setup
1. Create a **ClassLibrary** project inside your solution and call it **SolutionName.Tests**
2. Install these Packages on the test project.
    - xunit
    - xunit.runner.visualstudio
    - Microsoft.NET.Test.Sdk
    - FluentAssertions
    - Moq
3. In the Test project add refrences to other projects you want to test.
---

## Creating a Mock object for an Interface
If the class we testing has some dependencies on it's constructor we can mock those Interfaces.<br>
We have to crate a new **Mock\<InterfaceType>** and the use the it's **Object**.

```C#
[Fact]
public void Status_OnInitialize_ReturnsActive()
{
    // Creating a Mock object
    Mock<ICodeValidator> mockCodeValidator = new Mock<ICodeValidator>();

    // Arrange 
    var sut = new StudentService(mockCodeValidator.Object);

    // Assert
    sut.Status.Should().Be("Active");
}
```

---

## Mocking a method's result
If we need to mock a methods's result on the mocking object we can call **Setup** method and **Returns** method.
```C#
[Fact]
public void Create_OnExecute_ReturnsStudent()
{
    // Creating a Mock object
    Mock<ICodeValidator> mockCodeValidator = new Mock<ICodeValidator>();

    // Set the return value.
    mockCodeValidator
        .Setup(x => x.IsValid(15))
        .Returns(true);

    // Arrange 
    var sut = new StudentService(mockCodeValidator.Object);

    // Act
    var actualResult = sut.Create(15, "John", "Doe");

    // Assert
    actualResult.Should().NotBeNull();
}
```
Instead of sending a specific argument we can also mock the arguments like this .
```C#
mockCodeValidator
            .Setup(x => x.IsValid(It.IsAny<int>()))
            .Returns(true);
```
```C#
mockCodeValidator
            .Setup(x => x.IsValid(It.IsInRange<int>(10,20, Moq.Range.Inclusive)))
            .Returns(true);
```
```C#
mockCodeValidator
            .Setup(x => x.IsValid(It.IsIn<int>(10, 15, 18,20 )))
            .Returns(true);
```
```C#
mockCodeValidator
            .Setup(x => x.IsValid(It.IsRegex["[a-z]"]))
            .Returns(true);
```
---

## Mocking with Linq
we can also use linq to create a mock and setup methods.
```C#
//Mock<ICodeValidator> mockCodeValidator = new Mock<ICodeValidator>();
//mockCodeValidator.Setup(x => x.IsValid(It.IsAny<int>())).Returns(true);

//var sut = new StudentService(mockCodeValidator.Object);

ICodeValidator mockCodeValidator = Mock.Of<ICodeValidator>
    (
        codeValidator =>
        codeValidator.IsValid(It.IsAny<int>()) == true
    );

var sut = new StudentService(mockCodeValidator);
```
---

## Mocking a method with out parameter
```C#
// Creating a Mock object
Mock<ICodeValidator> mockCodeValidator = new Mock<ICodeValidator>();

bool isValid = true;

mockCodeValidator
    .Setup(x => x.IsValidWithOut(It.IsAny<int>(), out isValid));
```
---

## Verify a method calls count
We can check how many times a method calls.
```C#
// Creating a Mock object
Mock<ICodeValidator> mockCodeValidator = new Mock<ICodeValidator>();
mockCodeValidator.Setup(x => x.IsValid(It.IsAny<int>())).Returns(true);

var sut = new StudentService(mockCodeValidator.Object);

sut.Create(1, "John", "Doe");

//mockCodeValidator.Verify(x => x.IsValid(It.IsAny<int>()), Times.Never);
//mockCodeValidator.Verify(x => x.IsValid(It.IsAny<int>()), Times.Exactly(3));
mockCodeValidator.Verify(x => x.IsValid(It.IsAny<int>()), Times.Once);
```
---

## Verify a property Getter or Setter was called 
```C#
var mockCodeValidator = new Mock<ICodeValidator>();
mockCodeValidator.Setup(x => x.IsValid(It.IsAny<int>())).Returns(true);

var sut = new StudentService(mockCodeValidator.Object);

sut.Create(1, "John", "Doe");

// Verify a property Getter was called 
mockCodeValidator.VerifyGet(x => x.Status);
//mockCodeValidator.VerifyGet(x => x.Status, Times.Once);

// Verify a property Setter was called 
mockCodeValidator.VerifySet(x => x.Status);
//mockCodeValidator.VerifySet(x => x.Status = "new status");
mockCodeValidator.VerifySet(x => x.Status = It.IsAny<string>());
```
---