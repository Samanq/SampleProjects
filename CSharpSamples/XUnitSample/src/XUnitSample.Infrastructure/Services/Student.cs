namespace XUnitSample.Infrastructure.Services;

public class Student
{
    public string Name { get; set; } = string.Empty;

    public List<string> Courses { get; set; } = new List<string> 
    {
        "C#",
        "C++",
        "Java"
    };
}
