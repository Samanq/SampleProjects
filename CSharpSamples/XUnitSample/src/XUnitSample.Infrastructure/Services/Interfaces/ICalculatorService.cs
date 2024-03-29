﻿namespace XUnitSample.Infrastructure.Services.Interfaces;

public interface ICalculatorService
{
    public int AddTwoNumbers(int firstNumber, int secondNumber);
    public int SubtractTwoNumbers(int firstNumber, int secondNumber);
    public int MultiplyTwoNumbers(int firstNumber, int secondNumber);
    public double DivideTwoNumbers(double firstNumber, double secondNumber);
}
