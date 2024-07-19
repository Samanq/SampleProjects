# Linq Sample
LINQ (Language-Integrated Query) is a set of features in the .NET Framework that provides a consistent, object-oriented way to query data from different sources such as databases, XML documents, and in-memory collections. It allows developers to write queries using familiar language constructs such as C# and VB.NET, which are then translated into a common syntax that can be used to query any data source that supports LINQ.

LINQ provides a uniform way to work with different data sources by providing a standard query syntax and a set of query operators that can be used to filter, sort, group, and project data. It allows developers to write queries that are strongly typed, which means that the query expressions are checked for correctness at compile time rather than at runtime.

LINQ also provides support for deferred execution, which means that queries are not executed until the data is actually needed. This allows for efficient data processing, as only the required data is retrieved and processed.

Some common use cases for LINQ include:

- Querying databases using LINQ to SQL or Entity Framework
- Querying XML documents using LINQ to XML
- Querying in-memory collections using LINQ to Objects

Overall, LINQ provides a powerful and flexible way to work with data in a variety of scenarios, making it a valuable tool for developers working in the .NET ecosystem.

There are two different syntax to write LINQ queries.
 - **Query Syntax**: <br>
 The query syntax is a declarative syntax that resembles SQL and is used to write queries in a familiar syntax for database developers. It consists of a sequence of clauses that specify what data to retrieve and how to transform it.<br>
 For example:
 ```C#
 var result = from person in people                 // From every person in people list
             where person.Age > 30                  // Condition
             orderby person.LastName descending     // Sort
             select person;                         // Resut

 ```
- **Method or Fluent Syntax** <br>
Fluent syntax is a method chaining syntax that is more concise and readable. It consists of a series of extension methods that are called on a data source to filter, sort, and project data.<br>
For example:
```C#
var result = people.Where(person => person.Age > 30)                // Condition
                   .OrderByDescending(person => person.LastName)    // Sort
                   .Select(person => person);                       // Result

```
---
## SelectMany
Is a method that is used to perform a one-to-many or a many-to-many **projection** operation on a **sequence of elements**. It is often used when you have **nested collections** and want to **flatten** them or when you want to project elements from one collection into multiple elements in the result.
```C#
// SelectMany Sample
var courses = StudentService.GetAllStudents()
    .SelectMany(student => student.Courses)
    .Distinct(); // If you want to ignore the duplicated values

// Sample 2
// Create new items from the result.
var studentReports = StudentService.GetAllStudents()
    .SelectMany(student => student.Courses, (student, course) =>
        new StudentReport { StudentName = student.Name, CourseName = course });   
```
---

## Inner Join
Joining two different tables and create a new result.<br>
Query Syntax:
```C#
var employeeReports = from employee in employees                            // Left Table
                      join employeeType in employeeTypes                    // Right Table
                      on employee.EmployeeTypeId equals employeeType.Id     // Condition
                      select new EmployeeReport                             // Result
                      {
                          EmployeeName = employee.Name,
                          EmployeePosition = employeeType.Title
                      };
```
Fluent Syntax:
```C#
var secondEmployeeReports = employees.Join(employeeTypes,                                   // Left Table and Right Table
                                           employee => employee.EmployeeTypeId,             // Conditions
                                           employeeType => employeeType.Id,                 // Conditions
                                           (employee, employeeType) => new EmployeeReport   // Result
                                           {
                                               EmployeeName = employee.Name,
                                               EmployeePosition = employeeType.Title
                                           });
```
---

## GroupBy
GroupBy is an operator that is used to group a sequence of elements based on a key. <br>
Query Syntax:
```C#
var studentGroupsSorted = from student in StudentService.GetAllStudents()
        group student by student.GroupName into sGroup
        orderby sGroup.Key
        select new
        {
            Key = sGroup.Key,
            Students = sGroup
        };
```
Fluent Syntax:
```C#
IEnumerable<IGrouping<string, Student>> studentGroups = StudentService.GetAllStudents()
    .GroupBy(s => s.GroupName);

foreach (var group in studentGroups)
{
    Console.WriteLine(group.Key);
    foreach (var student in group)
    {
        Console.WriteLine($"\t {student.Name}");
    }
}

// OR
var studentGroupsSorted = StudentService.GetAllStudents()
    .GroupBy(student => student.GroupName)
    .OrderBy(studentGroup => studentGroup.Key) // Ordering by GroupName 
    .Select(studentGroup => new 
    {
        Key = studentGroup.Key,
        Students = studentGroup.OrderBy(student => student.Name) // Order by StudentName
    });
```

### Another explanation
1.Simple GroupBy Example:
```C#
using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        // Sample data
        var students = new List<Student>
        {
            new Student { Name = "Alice", Grade = "A" },
            new Student { Name = "Bob", Grade = "B" },
            new Student { Name = "Charlie", Grade = "A" },
            new Student { Name = "Dave", Grade = "B" },
            new Student { Name = "Eve", Grade = "C" }
        };

        // Group students by their grade
        var groupedStudents = students.GroupBy(s => s.Grade);

        // Iterate through each group
        foreach (var group in groupedStudents)
        {
            Console.WriteLine($"Grade: {group.Key}");
            foreach (var student in group)
            {
                Console.WriteLine($"  {student.Name}");
            }
        }
    }
}

public class Student
{
    public string Name { get; set; }
    public string Grade { get; set; }
}
```
2. GroupBy with Result Selector:
You can use a result selector to project the grouped results into a new form:

```C#
var groupedStudents = students.GroupBy(
    s => s.Grade,
    (key, group) => new { Grade = key, Students = group.ToList() }
);

foreach (var group in groupedStudents)
{
    Console.WriteLine($"Grade: {group.Grade}");
    foreach (var student in group.Students)
    {
        Console.WriteLine($"  {student.Name}");
    }
}

```
3. GroupBy with Multiple Keys:
If you need to group by multiple keys, you can use anonymous types:
```C#
var groupedStudents = students.GroupBy(
    s => new { s.Grade, Initial = s.Name[0] }
);

foreach (var group in groupedStudents)
{
    Console.WriteLine($"Grade: {group.Key.Grade}, Initial: {group.Key.Initial}");
    foreach (var student in group)
    {
        Console.WriteLine($"  {student.Name}");
    }
}

```

---