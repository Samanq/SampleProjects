namespace LinqSample.ConsoleApp;

public static class StudentService
{
    public static IEnumerable<Student> GetAllStudents()
    {
        var students = new List<Student>()
        {
            new Student
            {
                Name= "John", Age = 20, Courses = new List<string>{"C#","SQL","Python" }
            },
            new Student
            {
                Name= "Jane", Age = 22, Courses = new List<string>{"C++","SQL","UML" }
            },
            new Student
            {
                Name= "Peter", Age = 25, Courses = new List<string>{"Node","TypeScript","MongoDb" }
            },
        };
        return students;
    }
}
