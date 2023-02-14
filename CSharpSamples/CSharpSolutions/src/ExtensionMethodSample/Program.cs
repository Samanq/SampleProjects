using ExtensionMethodSample.Extensions;
using ExtensionMethodSample.Services;

// Using the default method in Calculator method.
Calculator calculator = new Calculator();
Console.WriteLine(calculator.AddTwoNumbers(1, 2)); 

// Using the extension method tha we wrote in CalculatorExtended class.
calculator.AddAndPrintNumbers(1, 2);