using XUnitSample.Infrastructure.Services.Interfaces;

namespace XUnitSample.Infrastructure.Services;

public class CalculatorService : ICalculatorService, IDisposable
{
    public string Type { get; set; } = "Classic";
    public bool IsOn { get; set; } = true;

    public int AddTwoNumbers(int firstNumber, int secondNumber)
    {
        return firstNumber + secondNumber;
    }

    public double DivideTwoNumbers(double firstNumber, double secondNumber)
    {
        if  (secondNumber == 0) 
        {
            throw new DivideByZeroException();
        }

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
