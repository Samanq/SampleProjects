namespace GenericSample.ConsoleApp
{
    // where T : IInterface
    // where T : Animal
    // where T : struct
    // where T : class
    // where T : new()      has default constructor
    internal class AnimalAgeCalculator<TAnimal> where TAnimal : Animal
    {
        public int CalculateAge(TAnimal animal)
        {
            if (animal is Cat)
            {
                return animal.ActualAge * 6;
            }
            else
            {
                return animal.ActualAge * 7;
            }
        }
    }
}
