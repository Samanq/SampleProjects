using XUnitSample.Infrastructure.Services.Interfaces;

namespace XUnitSample.Infrastructure.Services;

public class CalculatorService : ICalculatorService, IDisposable
{
    public string Type { get; set; } = "Classic";
    public int AddTwoNumbers(int firstNumber, int secondNumber)
    {
        return firstNumber + secondNumber;
    }

    public int DivideTwoNumbers(int firstNumber, int secondNumber)
    {
        return firstNumber / secondNumber;
    }

    public int MultiplyTwoNumbers(int firstNumber, int secondNumber)
    {
        return firstNumber * secondNumber;
    }

    public int SubtractTwoNumbers(int firstNumber, int secondNumber)
    {
        return firstNumber - secondNumber;
    }

    public void Dispose()
    {
        
    }
}
