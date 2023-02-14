using Xunit;
using XUnitSample.Infrastructure.Services;

namespace XUnitSample.Tests
{
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

        [Fact]
        public void AddTwoNumbers_SimpleNumbers_ReturnsSum()
        {
            var actualValue = _sut.AddTwoNumbers(3, 2);
            Assert.Equal(5, actualValue);
        }

    }
}
