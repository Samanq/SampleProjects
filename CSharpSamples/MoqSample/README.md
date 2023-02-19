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

---
