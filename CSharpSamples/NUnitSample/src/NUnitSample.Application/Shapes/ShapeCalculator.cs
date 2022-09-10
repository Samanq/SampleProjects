using NUnitSample.Application.Interfaces;

namespace NUnitSample.Application.Shapes;

public class ShapeCalculator
{
    public double AggregateAreas(List<IShape> shapes)
    {
        var areas = 0.0;

        foreach (IShape shape in shapes)
        {
            areas += shape.CalculateArea();
        }

        return areas;
    }
}
