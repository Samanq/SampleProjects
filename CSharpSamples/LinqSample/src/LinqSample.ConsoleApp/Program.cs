using LinqSample.ConsoleApp;


// SelectMany Sample
var courses = StudentService.GetAllStudents()
    .SelectMany(student => student.Courses)
    .Distinct(); // If you want to ignore the duplicated values

foreach (var course in courses)
{
    Console.WriteLine(course);
}


