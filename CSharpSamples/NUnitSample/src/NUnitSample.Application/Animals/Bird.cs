namespace NUnitSample.Application.Animals;
using NUnitSample.Application.Interfaces;
public class Bird : IAnimal
{
    public string LastWord { get; set; }

    public void Move()
    {
        Console.WriteLine("The bird strat flying...");
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
