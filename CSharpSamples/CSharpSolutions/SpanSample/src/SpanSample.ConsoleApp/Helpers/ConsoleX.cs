namespace SpanSample.ConsoleApp.Helpers;
public static class ConsoleX
{
    public static void DrawSeparator()
    {
        Console.ResetColor();

        Console.WriteLine("\n------------------------------------------------------------------------");
    }

    public static void WriteLine(ReadOnlySpan<char> value)
    {
        foreach (char c in value)
        {
            Console.Write(c);
        }
        Console.Write('\n');
    }
}