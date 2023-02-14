using ExtensionMethodSample.Services;

namespace ExtensionMethodSample.Extensions;

// Extension class shoud be static
static class CalculatorExtended
{
    // Extension method must be static 
    // It must use this keyword and Target class as the first parameter
    public static void AddAndPrintNumbers(this Calculator calculator, int firstNumber, int secondNumber)
    {
        int result = calculator.AddTwoNumbers(firstNumber, secondNumber);
        Console.WriteLine($"{firstNumber} + {secondNumber} = {result}");
    }
}
