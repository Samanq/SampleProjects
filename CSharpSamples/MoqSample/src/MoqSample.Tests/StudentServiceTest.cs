using FluentAssertions;
using Moq;
using MoqSample.Infrastructure.Services;
using MoqSample.Infrastructure.Services.Interfaces;

namespace MoqSample.Tests;

public class StudentServiceTest
{
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

    [Fact]
    public void Create_OnExecute_ReturnsStudent_WithArgument()
    {
        // Creating a Mock object
        Mock<ICodeValidator> mockCodeValidator = new Mock<ICodeValidator>();

        // Set the return value.
        mockCodeValidator
            .Setup(x => x.IsValid(It.IsAny<int>()))
            .Returns(true);

        mockCodeValidator
            .Setup(x => x.IsValid(It.IsInRange<int>(10, 20, Moq.Range.Inclusive)))
            .Returns(true);

        mockCodeValidator
            .Setup(x => x.IsValid(It.IsIn<int>(10, 15, 18,20 )))
            .Returns(true);

        // Arrange 
        var sut = new StudentService(mockCodeValidator.Object);

        // Act
        var actualResult = sut.Create(15, "John", "Doe");

        // Assert
        actualResult.Should().NotBeNull();
    }

    [Fact]
    public void Create_OnExecute_ReturnsStudent_WithOutParameter()
    {
        // Creating a Mock object
        Mock<ICodeValidator> mockCodeValidator = new Mock<ICodeValidator>();

        bool isValid = true;
        
        mockCodeValidator
            .Setup(x => x.IsValidWithOut(It.IsAny<int>(), out isValid));

        // Arrange 
        var sut = new StudentService(mockCodeValidator.Object);

        // Act
        var actualResult = sut.Create(15, "John", "Doe");

        // Assert
        actualResult.Should().NotBeNull();
    }
}