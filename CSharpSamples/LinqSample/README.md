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