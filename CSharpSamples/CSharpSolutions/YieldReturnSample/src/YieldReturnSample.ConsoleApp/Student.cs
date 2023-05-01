namespace YieldReturnSample.ConsoleApp;

public class Student
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }

    public IEnumerable<string> Courses
    {
        get
        {
            yield return "C#";
            yield return "SQL";
            yield return "Python";
        }
    }
}
