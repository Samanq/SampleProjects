namespace NUnitSample.Application.Shapes;

using NUnitSample.Application.Interfaces;

public class Circle : IShape
{
    public double Radius { get; }


    public Circle(double radius)
    {
        Radius = radius;
    }

    /// <summary>
    /// Calculate the area of the circle.
    /// </summary>
    /// <returns>Returns A = pi*r^2</returns>
    public double CalculateArea()
    {
        return System.Math.PI * (Radius * Radius);
    }
}
