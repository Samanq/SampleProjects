using System;

namespace AsyncSample.WebApplication.Services;

public static class MathService
{
    public static string GetPrimesNumbers(int start, int end)
    {
        List<int> primeNumbers = new List<int>();

        for (int num = start; num <= end; num++)
        {
            if (IsPrime(num))
            {
                primeNumbers.Add(num);
            }
        }

        return $"There are {primeNumbers.Count} prime numbers between {start} and {end}";
        
    }

    private static bool IsPrime(int number)
    {
        if (number <= 1)
            return false;

        if (number <= 3)
            return true;

        if (number % 2 == 0 || number % 3 == 0)
            return false;

        for (int i = 5; i * i <= number; i += 6)
        {
            if (number % i == 0 || number % (i + 2) == 0)
                return false;
        }

        return true;
    }
}
