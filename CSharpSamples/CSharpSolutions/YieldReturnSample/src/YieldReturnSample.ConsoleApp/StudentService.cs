namespace YieldReturnSample.ConsoleApp;

public class StudentService
{
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
}
