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
            .Setup(x => x.IsValid(It.IsIn<int>(10, 15, 18, 20)))
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

    [Fact]
    public void Create_OnExecute_ReturnsStudent_ValidateCount()
    {
        // Creating a Mock object
        Mock<ICodeValidator> mockCodeValidator = new Mock<ICodeValidator>();
        mockCodeValidator.Setup(x => x.IsValid(It.IsAny<int>())).Returns(true);

        var sut = new StudentService(mockCodeValidator.Object);

        sut.Create(1, "John", "Doe");

        //mockCodeValidator.Verify(x => x.IsValid(It.IsAny<int>()), Times.Never);
        //mockCodeValidator.Verify(x => x.IsValid(It.IsAny<int>()), Times.Exactly(3));
        mockCodeValidator.Verify(x => x.IsValid(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void CheckStatus_OnExecute_CheckStatusProperty()
    {
        var mockCodeValidator = new Mock<ICodeValidator>();
        mockCodeValidator.Setup(x => x.IsValid(It.IsAny<int>())).Returns(true);

        var sut = new StudentService(mockCodeValidator.Object);

        sut.Create(1, "John", "Doe");

        // Verify a property Getter was called 
        mockCodeValidator.VerifyGet(x => x.Status);
        //mockCodeValidator.VerifyGet(x => x.Status, Times.Once);


        // Verify a property Setter was called 
        mockCodeValidator.VerifySet(x => x.Status = "new status");
        //mockCodeValidator.VerifySet(x => x.Status = It.IsAny<string>());
    }

    [Fact]
    public void UseLinq()
    {
        //Mock<ICodeValidator> mockCodeValidator = new Mock<ICodeValidator>();
        //mockCodeValidator.Setup(x => x.IsValid(It.IsAny<int>())).Returns(true);

        //var sut = new StudentService(mockCodeValidator.Object);

        ICodeValidator mockCodeValidator = Mock.Of<ICodeValidator>
            (
                codeValidator =>
                codeValidator.IsValid(It.IsAny<int>()) == true
            );
        
        var sut = new StudentService(mockCodeValidator);

        var result = sut.Create(1, "John", "Doe");

        
        result.Should().NotBeNull();
    }
}