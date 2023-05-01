using BenchmarkDotNet.Running;
using YieldReturnSample.ConsoleApp;

//var studentService = new StudentService();

//IEnumerable<Student> students = studentService.GenerateStudentNormally(100);
//foreach (Student student in students)
//{
//    Console.WriteLine($"{student.Id} - {student.Name}, {student.Age}");
//}


//IEnumerable<Student> studentsYield = studentService.GenerateStudentYield(100);
//foreach (Student student in studentsYield)
//{
//    Console.WriteLine($"{student.Id} - {student.Name}, {student.Age}");
//}


BenchmarkRunner.Run<GetStudentServiceBenchmark>();
//BenchmarkRunner.Run<GetArticleServiceBenchmark>();






//var articleLines = studentService.GetArticleLinesNormally();
//foreach (var line in articleLines)
//{
//    Console.WriteLine(line);
//}

//var articleLinesYield = studentService.GetArticleLinesYield();
//// We should await the iteration.
//await foreach (var line in articleLinesYield)
//{
//    Console.WriteLine(line);
//}

Console.ReadLine();


