using GenericSample.ConsoleApp;


Dog dog = new Dog() { ActualAge = 3, Name = "Puppy"};
Cat cat = new Cat() { ActualAge = 3, Name = "Kitten"};

var dogCalculator = new AnimalAgeCalculator<Dog>();
var catCalculator = new AnimalAgeCalculator<Cat>();

var dogAge = dogCalculator.CalculateAge(dog);
var catAge = catCalculator.CalculateAge(cat);

Console.WriteLine($"{dog.Name} age is {dogAge}");
Console.WriteLine($"{cat.Name} age is {catAge}");
