namespace LinqSample.ConsoleApp;

public class Student
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string GroupName { get; set; } = string.Empty;

    public List<string>? Courses { get; set; }
}
