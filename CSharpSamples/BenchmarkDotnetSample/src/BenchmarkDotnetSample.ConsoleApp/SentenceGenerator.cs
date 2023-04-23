using System.Collections.Specialized;
using System.Text;

namespace BenchmarkDotnetSample.ConsoleApp;

public class SentenceGenerator
{
    public string GenerateSentenceOriginal()
    {
        string sentence = string.Empty;

        for (int i = 0; i < 10; i++) 
        {
            sentence += "i";
        }

        return sentence;
    }

    public string GenerateSentenceWhitWhile()
    {
        string sentence = string.Empty;
        int counter = 0;

        while (counter < 10) 
        {
            sentence += counter;
            counter++;
        }

        return sentence;
    }

    public string GenerateSentenceWithStringBuilder() 
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < 10; i++)
        {
            stringBuilder.Append(i);
        }

        return stringBuilder.ToString();
    }
}
