using LinqSample.ConsoleApp;


// SelectMany Sample
var courses = StudentService.GetAllStudents()
    .SelectMany(student => student.Courses)
    .Distinct(); // If you want to ignore the duplicated values

foreach (var course in courses)
{
    Console.WriteLine(course);
}

// SelectMany Sample 
// Create a items from the result.
var studentReports = StudentService.GetAllStudents()
    .SelectMany(student => student.Courses, (student, course) =>
        new StudentReport { StudentName = student.Name, CourseName = course });

Console.WriteLine("--------------------------------------------------------");
Console.WriteLine("Student Reports: ");
foreach (var studentReport in studentReports)
{
    Console.WriteLine($"{studentReport.StudentName} - {studentReport.CourseName}");
}


