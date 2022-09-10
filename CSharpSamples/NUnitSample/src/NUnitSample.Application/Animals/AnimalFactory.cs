namespace NUnitSample.Application.Animals;
using NUnitSample.Application.Interfaces;

public class AnimalFactory
{
    public IAnimal CreateAnimal(bool canFly)
    {
        return canFly ? new Bird() : new Dog();
    }
}
