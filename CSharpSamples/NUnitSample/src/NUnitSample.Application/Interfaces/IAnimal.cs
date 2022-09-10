namespace NUnitSample.Application.Interfaces;
public interface IAnimal
{
    public string LastWord { get; set; }
    public void Move();
    public void Speak(string sentence);
}
