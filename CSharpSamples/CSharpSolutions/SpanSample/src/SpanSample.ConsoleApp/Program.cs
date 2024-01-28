using BenchmarkDotNet.Running;
using SpanSample.ConsoleApp.Benchmarks;
using SpanSample.ConsoleApp.Helpers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

//Case01.Run();
//Case02.Run();
//Case03.Run();
//Case04.Run();
//Case05.Run();
//Case06.Run();

//BenchmarkRunner.Run<Case01Benchmark>();
//BenchmarkRunner.Run<Case02Benchmark>();
//BenchmarkRunner.Run<Case03Benchmark>();
//BenchmarkRunner.Run<Case04Benchmark>();
//BenchmarkRunner.Run<Case05Benchmark>();
BenchmarkRunner.Run<Case06Benchmark>();


//int[] numberArray = new int[10];
//Span<int> numberSpan = new Span<int>(numberArray);


//TestClass.IterateNumber();
//TestClass2.IterateNumber();

//Case03.Run();
//Case04.Run();


public static class Case01
{
    public static void Run()
    {
        string str = "hello, world";

        string worldString = str.Substring(startIndex: 7, length: 5); // With heap allocation

        ReadOnlySpan<char> worldSpan = str.AsSpan().Slice(start: 7, length: 5); // Without heap allocation

        Console.WriteLine(worldString);
        ConsoleX.WriteLine(worldSpan);
    }
}

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

public static class Case03
{
    static int[] originalNumberArray = Enumerable.Range(1, 100).ToArray();

    public static void Run()
    {
        ConsoleX.DrawSeparator();
        Console.WriteLine("Test03\n");
        int[] newNumberArray = originalNumberArray[0..5];
        Span<int> newNumberSpan = originalNumberArray.AsSpan()[0..5];

        originalNumberArray[0] = -1;    // Changing the value of original numbers.

        Console.WriteLine("original");
        foreach (var item in originalNumberArray[0..5])
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("\nnewNumberArray");
        foreach (var item in newNumberArray)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("\nnewNumberSpan");
        foreach (var item in newNumberSpan)
        {
            Console.WriteLine(item);
        }
    }
}

public static class Case04
{
    public static void Run()
    {
        // unsafe
        // {
        //     int* numbersInStack = stackalloc int[] { 1, 2, 3, 4, 5 };
        // }

        int[] numbersInHeap = [1, 2, 3, 4, 5];
        Span<int> numbersInStack = stackalloc int[] { 6, 7, 8, 9, 10 };


        Console.WriteLine("");
    }
}

public static class Case05
{
    public static void Run()
    {

    }
}

public static class Case06
{
    public static void Run()
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\LargeFile.txt");

        using (var sr = new StreamReader(path))
        {
            string text = sr.ReadToEnd(); // Allocates a new string object
            byte[] buffer = Encoding.UTF8.GetBytes(text);
        }
    }
}