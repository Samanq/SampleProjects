namespace NUnitSample.Application.Animals;
using NUnitSample.Application.Interfaces;

public class Dog : IAnimal
{
    public string LastWord { get; set; }

    public void Move()
    {
        Console.WriteLine("Dog started walking...");
    }

    public void Speak(string sentence)
    {
        if (string.IsNullOrWhiteSpace(sentence))
        {
            throw new ArgumentNullException();
        }

        LastWord = sentence;
        Console.WriteLine($"Bird is saying: {sentence}");
    }
}
