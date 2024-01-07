using BenchmarkDotNet.Running;
using SpanSample.ConsoleApp;
using System.Runtime.InteropServices;

//BenchmarkRunner.Run<FirstBenchmark>();

//int[] numberArray = new int[10];
//Span<int> numberSpan = new Span<int>(numberArray);


TestClass.IterateNumber();
//TestClass2.IterateNumber();

public static class TestClass
{
    public static void IterateNumber()
    {
        List<int> numberList = Enumerable.Range(1, 10).ToList();
        Span<int> numberSpan = CollectionsMarshal.AsSpan(numberList);

        Console.WriteLine("First Iteration of Span");
        for (int i = 0; i < numberSpan.Length; i++) 
        {
            numberSpan[i] *= 2;
            Console.WriteLine(numberSpan[i]);
            numberList[i] *= 5;     // Changing the original list
        }

        Console.WriteLine("\nSecond Iteration of Span");
        foreach (var item in numberSpan) 
        {
            Console.WriteLine(item);
        }
    }
}






public static class TestClass2
{
    public static void IterateNumber()
    {
        List<int> numberList = Enumerable.Range(1, 10).ToList();
        int[] numberArray = numberList.ToArray();


        Console.WriteLine("First Iteration of Array");
        for (int i = 0; i < numberArray.Length; i++)
        {
            numberArray[i] *= 2;
            Console.WriteLine(numberArray[i]);
            numberList[i] *= 5;     // Changing the original list
        }

        Console.WriteLine("\nSecond Iteration of Array");
        foreach (var item in numberArray)
        {
            Console.WriteLine(item);
        }
    }
}