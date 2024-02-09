using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using SpanSample.ConsoleApp.Benchmarks;
using SpanSample.ConsoleApp.Helpers;
using System.Buffers;
using System.Runtime.InteropServices;

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
BenchmarkRunner.Run<Case05Benchmark>();
//BenchmarkRunner.Run<Case06Benchmark>();
//BenchmarkRunner.Run<AdditionalBenchmark01>();

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

public static class Case02
{
    public static void Run()
    {
        IterateNumberWithArray();
        IterateNumberWithSpan();
    }
    public static void IterateNumberWithArray()
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
    public static void IterateNumberWithSpan()
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
        UsingUnsafe();
        UsingSpan();
    }
    static void UsingUnsafe()
    {
        const int length = 5;
        unsafe
        {
            int* numbersInStackPointer = stackalloc int[length] { 1, 2, 3, 4, 5 };

            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(numbersInStackPointer[i]);
            }
        }
    }

    static void UsingSpan()
    {
        // Declaring the array in the stack
        Span<int> numbersInStack = stackalloc int[] { 1, 2, 3, 4, 5 };

        foreach (var number in numbersInStack)
        {
            Console.WriteLine(number);
        }
    }
}

public static class Case05
{
    public static void Run()
    {
        Guid id = Guid.NewGuid();
        string base64Id = Convert.ToBase64String(id.ToByteArray());

        Console.WriteLine($"{id} \t\t Guid");
        Console.WriteLine($"{base64Id} \t\t\t Base64String\n");


        string friendlyBase64 = GuidHelper.GuidToFriendlyString(id);
        string friendlyBase64WithSpan = GuidHelper.GuidToFriendlyStringWithSpan(id);

        Console.WriteLine($"{friendlyBase64} \t\t\t\t Guid to friendly string.");
        Console.WriteLine($"{friendlyBase64WithSpan} \t\t\t\t Guid to friendly string with Span.\n");


        Guid guidFromFriendlyString = GuidHelper.FriendlyStringToGuid(friendlyBase64);
        Guid guidFromFriendlyStringWithSpan = GuidHelper.FriendlyStringToGuidWithSpan(friendlyBase64WithSpan);


        Console.WriteLine($"{guidFromFriendlyString} \t\t Friendly string to Guid.");
        Console.WriteLine($"{guidFromFriendlyStringWithSpan} \t\t Friendly string to Guid with Span.");
    }
}

public static class Case06
{
    private static string _code = "TST-102321";
    public static void Run()
    {
        Console.WriteLine(StartWith());
        Console.WriteLine(StartWithSpan());
        Console.WriteLine(StartWithSpanLoop());
    }

    public static bool StartWith()
    {
        return _code.StartsWith("TST");
    }


    public static bool StartWithSpan()
    {

        Span<char> chars = stackalloc char[3];
        chars[0] = 'T';
        chars[1] = 'S';
        chars[2] = 'T';

        return _code.AsSpan(0, 3).StartsWith(chars);
    }

    public static bool StartWithSpanLoop()
    {
        Span<char> chars = stackalloc char[3];
        chars[0] = 'T';
        chars[1] = 'S';
        chars[2] = 'T';

        ReadOnlySpan<char> input = _code.AsSpan(0, 3);

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != chars[i])
            {
                return false;
            }
        }

        return true;
    }
}
