namespace NUnitSample.Application.Shapes;

using NUnitSample.Application.Interfaces;

public class Rectangle : IShape
{
    public double Lenght { get; }
    public double Width { get; }

    public Rectangle(double lenght, double width)
    {
        Lenght = lenght;
        Width = width;
    }

    /// <summary>
    /// Calculate the area of the rectangle
    /// </summary>
    /// <returns>Lenght * Width = Area</returns>
    public double CalculateArea()
    {
        return Width * Lenght;
    }
}
