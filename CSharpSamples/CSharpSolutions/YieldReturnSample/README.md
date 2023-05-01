# Yield Return Sample

In C#, **yield return** is a feature that allows a method to return a **sequence of values** to the caller, **one at a time**, rather than returning all the values at once. When a method uses yield return, it becomes an **iterator**, and the values it produces can be consumed by a foreach loop or other iteration constructs.

---
## Simple Sample
With the help of **yield** we can return items one by one and we don't need a list variable to store all items and return them all at once.<br>
Somehow we can can say that we can create Lazy iterator with yield.


```C#
public IEnumerable<Student> GenerateStudentNormally(int count)
{
    List<Student> students = new List<Student>();

    Random rnd = new Random();

    for (int i = 0; i < count; i++)
    {
        students.Add(new Student
        {
            Id = i,
            Name = $"Student {i}",
            Age = rnd.Next(10, 30)
        });
    }

    return students;
}

public IEnumerable<Student> GenerateStudentYield(int count)
{
    // We don't need a list anymore.
    //List<Student> students = new List<Student>();

    Random rnd = new Random();

    for (int i = 0; i < count; i++)
    {
        // We are returing every student one by one
        yield return new Student
        {
            Id = i,
            Name = $"Student {i}",
            Age = rnd.Next(10, 30)
        };
    }
}
```
We can compare the benchmark of these two methods.<br>
![Benchmark Image](assets/images/benchmark.png)

---

## Yield properties
We can also use return yield for properties.
```C#
public class Student
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

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
```

---

## Async Yield

For using yield return in async method, we should change the return type to **IAsyncEnumerable** and using **async** when we want to iterate it.
```C#
public IEnumerable<string> GetArticleLinesNormally()
{
    List<string> lines = new List<string>();

    string workingDirectory = Environment.CurrentDirectory;
    string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
    string filePath = Path.Combine(projectDirectory, "assets", "article.txt");

    using (StreamReader streamReader = new StreamReader(filePath))
    {
        string line;
        while ((line = streamReader.ReadLine()) != null)
        {
            Thread.Sleep(100);
            lines.Add(line);
        }
    }

    return lines;
}

// We should use IAsyncEnumerable instead of Task<IEnumerable<string>>
public async IAsyncEnumerable<string> GetArticleLinesYield()
{
    // We don't need the list here anymore
    //List<string> lines = new List<string>();

    string workingDirectory = Environment.CurrentDirectory;
    string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
    string filePath = Path.Combine(projectDirectory, "assets", "article.txt");

    using (StreamReader streamReader = new StreamReader(filePath))
    {
        string line;
        // We are using await here
        while ((line = await streamReader.ReadLineAsync()) != null)
        {
            await Task.Delay(100);
            yield return line;
        }
    }
}
```
```C#
var articleLines = studentService.GetArticleLinesNormally();
foreach (var line in articleLines)
{
    Console.WriteLine(line);
}

var articleLinesYield = studentService.GetArticleLinesYield();
// We should await the iteration.
await foreach (var line in articleLinesYield)
{
    Console.WriteLine(line);
}
```