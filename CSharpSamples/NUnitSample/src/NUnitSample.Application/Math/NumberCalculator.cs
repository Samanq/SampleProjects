namespace NUnitSample.Application.Math;

public class NumberCalculator
{
    public double AddTowNumber(double firstNumber, double secondNumber)
    {
        return firstNumber + secondNumber;
    }
    public double SubtractTowNumber(double firstNumber, double secondNumber)
    {
        return firstNumber - secondNumber;
    }

    public IEnumerable<int> GetOddNumbers(int firstNumber, int secondNumber)
    {
        var result = new List<int>();

        for (int i = firstNumber; i <= secondNumber; i++)
        {
            if (i % 2 != 0) result.Add(i);
        }

        return result;
    }

    public IEnumerable<int> DistinctNumbers(int[] numbers)
    {
        return numbers.Distinct();
    }

    public IEnumerable<int> SortNumbersAscending(int[] numbers)
    {
        Array.Sort(numbers);

        return numbers;
    }

    public IEnumerable<int> SortNumbersDescending(int[] numbers)
    {
        Array.Sort(numbers);
        Array.Reverse(numbers);

        return numbers;
    }
}
