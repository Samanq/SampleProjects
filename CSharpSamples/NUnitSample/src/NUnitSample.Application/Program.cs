using NUnitSample.Application;
using NUnitSample.Application.Interfaces;
using NUnitSample.Application.Shapes;

Console.WriteLine("Hello, World!");


var circle = new Circle(18);
var circleArea = circle.CalculateArea();

var rectangle = new Rectangle(15, 20);
var rectangleArea = rectangle.CalculateArea();

Console.WriteLine($"Circle Area: {circleArea}");
Console.WriteLine($"Rectangle Area: {rectangleArea}");


List<IShape> shapes = new List<IShape>
{
    new Rectangle(10,8),
    new Rectangle(12.5,7.5)
};


// Reading text
TextService textService = new TextService();

var text = textService.ReadTextFromFile();
Console.WriteLine(text);

Console.ReadKey();