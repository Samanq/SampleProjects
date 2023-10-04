namespace LinqSample.ConsoleApp;

public static class StudentService
{
    public static IEnumerable<Student> GetAllStudents()
    {
        var students = new List<Student>()
        {
            new Student
            {
                Name= "John", Age = 20, GroupName = "IT", Courses = new List<string>{"C#","SQL","Python" }
            },
            new Student
            {
                Name= "Jane", Age = 22, GroupName = "IT", Courses = new List<string>{"C++","SQL","UML" }
            },
            new Student
            {
                Name= "Peter", Age = 25, GroupName = "IT", Courses = new List<string>{"Node","TypeScript","MongoDb" }
            },

            new Student
            {
                Name= "Saman", Age = 20, GroupName = "Grpahic", Courses = new List<string>{"Maya","Blender","3dsMax" }
            },
            new Student
            {
                Name= "Parisa", Age = 22, GroupName = "Grpahic", Courses = new List<string>{"UE5","BluePrint","C++" }
            },
            new Student
            {
                Name= "Pico", Age = 25, GroupName = "Grpahic", Courses = new List<string>{"Rendering","Lighting","Deploying" }
            }
        };
        return students;
    }
}
