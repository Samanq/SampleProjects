using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using XUnitSample.Infrastructure.Services;
using XUnitSample.Tests.ExternalTestData;
using XUnitSample.Tests.InternalTestData;
using XUnitSample.Tests.TestDataAttributes;

namespace XUnitSample.Tests
{
    public class CalculatorServiceTest : IDisposable
    {
        private readonly CalculatorService _sut;
        private readonly ITestOutputHelper _output;

        public CalculatorServiceTest(ITestOutputHelper output)
        {
            _sut = new CalculatorService();
            _output = output;
        }
        public void Dispose()
        {
            _sut.Dispose();
        }

        [Fact]
        public void AddTwoNumbers_SimpleNumbers_ReturnsSum()
        {
            // Using Custom output message.
            _output.WriteLine("Test started.");

            var actualValue = _sut.AddTwoNumbers(3, 2);
            Assert.Equal(5, actualValue);

            //Assert.Equal("classic",sut.Type,ignoreCase:true);
            _sut.Type.Should().BeEquivalentTo("classic");

            //Assert.Contains("Clas", sut.Type);
            _sut.Type.Should().Contain("Clas");

            _sut.Type.Should().ContainEquivalentOf("clas");

            //Assert.StartsWith("Cl", sut.Type);
            _sut.Type.Should().StartWith("Cl");

            //Assert.EndsWith("ic", sut.Type);
            _sut.Type.Should().EndWith("ic");

            _sut.Type.Should().BeOneOf("Classic", "pro");

            //Assert.Matches("[A-Z]", sut.Type);
            _sut.Type.Should().MatchRegex("[A-Z]");

            _sut.Type.Should().Match("*s*");
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(2, 1, 3)]
        [InlineData(-1, -1, -2)]
        public void AddTwoNumbers_OnExecuteWithInlineData_ReturnSum(int firstNumber, int secondNumber, int expectedResult)
        {
            // Arrange
            var sut = new CalculatorService();

            // Act
            var result = sut.AddTwoNumbers(firstNumber, secondNumber);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [MemberData(
            nameof(InternalNumbersTestData.SumTestData),
            MemberType = typeof(InternalNumbersTestData))]
        public void AddTwoNumbers_OnExecuteWithMemberData_ReturnSum(int firstNumber, int secondNumber, int expectedResult)
        {
            // Arrange
            var sut = new CalculatorService();

            // Act
            var result = sut.AddTwoNumbers(firstNumber, secondNumber);

            // Assert
            Assert.Equal(expectedResult, result);
        }

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

    }
}
