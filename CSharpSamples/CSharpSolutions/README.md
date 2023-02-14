# CSharpSolutions
Some Fundementals in and some problems and solutions in C#.

---

## Extension Methods
Some times we need to add a method to existing classes which we dont't have access to modify them, so we decide to extend them.<br>
For creating an method we have to creat a **static** class and **static** method, for the parameter must you **this** keyword and the class we want to extend.<br>
If we want more prameter we can add after **this** TargetClass.<br>

Assume there is a class named Calculator and we dont have access to modify this class.
```C#
namespace ExtensionMethodSample.Services;

public class Calculator
{
    public int AddTwoNumbers(int firstNumber, int secondNumber)
    {
        return firstNumber + secondNumber;
    }
}
```
Now we want to extend this class and add another method for this class.<br>
We will create a **static** class and **static** method and as the first parameter we write **this** keyword and TargetClass.
```C#
using ExtensionMethodSample.Services;

namespace ExtensionMethodSample.Extensions;

// Extension class shoud be static
static class CalculatorExtended
{
    // Extension method must be static 
    // It must use this keyword and Target class as the first parameter
    public static void AddAndPrintNumbers(this Calculator calculator, int firstNumber, int secondNumber)
    {
        int result = calculator.AddTwoNumbers(firstNumber, secondNumber);
        Console.WriteLine($"{firstNumber} + {secondNumber} = {result}");
    }
}
```
In the end we can see that now the Calculator class has another method in **Program.cs**
```C#
using ExtensionMethodSample.Extensions;
using ExtensionMethodSample.Services;

// Using the default method in Calculator method.
Calculator calculator = new Calculator();
Console.WriteLine(calculator.AddTwoNumbers(1, 2)); 

// Using the extension method tha we wrote in CalculatorExtended class.
calculator.AddAndPrintNumbers(1, 2);
```

---


## Mapping Sample
---
## Generic Repository Sample
### steps
1. Create a Web API project.

2. Install these packages
    - Microsoft.EntityFrameworkCore.InMemory

3. Create a folder named Entities.

4. Inside Entities folder create a entity class

```C#
namespace GenericRepositorySample.Entities;
using System.ComponentModel.DataAnnotations;

public class Students
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateTime BirthDate { get; set; }
}
```

5. Create a folder named Data.

6. Inside Data folder create DataContext Class set the DbSets.
```c#
namespace GenericRepositorySample.Data;

using GenericRepositorySample.Entities;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options) 
		: base(options) {}

	// Defining DbSets
	public DbSet<Student> Students => Set<Student>();
}

```
---
## Delegate Sample
- Delegate is a <ins>type</ins> that holds a <ins>reference</ins> to  <ins>methods</ins> with a particular parameter list and return type.
- Delegate defines the rules of using.

We have a DocumentReader class that is responsiple for returning the documents and it has a method for filtering documents.

The method requires a list of documents and a instructure for filtering the documents.

 In this example our delegate says that the filtering function should gets a document and retun a boolean value.

 ### Defining the delegate.
```c#
public class DocumentReader
{
    // Functions that using this delegete must get a document and return a boolean value.
    public delegate bool DocumentFilterHandler(Document document);

    public List<Document> FilterDocuments(IEnumerable<Document> documents, DocumentFilterHandler filter)
    {
        List<Document> filteredDocuments = new List<Document>();

        foreach (var document in documents)
        {
            // If the result of the function that passes to this method is true.
            if (filter(document))
            {
                filteredDocuments.Add(document);
            }
        }

        return filteredDocuments;
    }
}
```
In DocumentFilters class we defined some methods that they are following the delegete rule inside the DocumentReader class.

### Defining the filters
```c#
public static class DocumentFilters
{
    // All methods get a document a return a boolean value like the delegate defines inside the DocumentReaderClass.
    public static bool IsTest(Document document)
    {
        return document.Description
            .ToLower()
            .Contains("#test");
    }
    public static bool IsImportant(Document document)
    {
        return document.Description
            .ToLower()
            .Contains("#important");
    }
    public static bool IsArchive(Document document)
    {
        return document.Description
            .ToLower()
            .Contains("#archive");
    }
}
```
In the program class when we want to filter our documents, we pass one of our filters to the FilterDocuments Method.

### Using the FilterDocuments method
```c#
DocumentReader documentReader = new DocumentReader();

// documents parameter is a list of documents we created earlier
var importantDocument = documentReader.FilterDocuments(documents, DocumentFilters.IsImportant);

Console.WriteLine("Important Documents:");
foreach (var document in importantDocument)
{
    Console.WriteLine($"{document.Title}\n{document.Description}\n");
}
```

---
## Anonymous Method Sample
- Anonymous method is a method <ins>without a name</ins>.
- Anonumous method can be defined by delegate keyword and can be assigned to a variable of type delegate.

 ### Defining the delegate.
```c#
public class DocumentReader
{
    // Functions that using this delegete must get a document and return a boolean value.
    public delegate bool DocumentFilterHandler(Document document);

    public List<Document> FilterDocuments(IEnumerable<Document> documents, DocumentFilterHandler filter)
    {
        List<Document> filteredDocuments = new List<Document>();

        foreach (var document in documents)
        {
            // If the result of the function that passes to this method is true.
            if (filter(document))
            {
                filteredDocuments.Add(document);
            }
        }

        return filteredDocuments;
    }
}
```

### Using the FilterDocuments method with anonymouse method
```c#
DocumentReader documentReader = new DocumentReader();

// This is a anonymouse function.
// It doesn't have a name and it defines by delegate keyword
DocumentFilterHandler importantFilter = delegate (Document document)
{
    return document.Description
    .ToLower()
    .Contains("#important");
};

// documents parameter is a list of documents we created earlier
var importantDocument = documentReader.FilterDocuments(documents, importantFilter);

Console.WriteLine("Important Documents:");
foreach (var document in importantDocument)
{
    Console.WriteLine($"{document.Title}\n{document.Description}\n");
}
Console.WriteLine("--------------------------------");
```
---

### Lambda Expression

For defining anonymouse functions with lambda expression.

#### Intead of this
```c#
DocumentFilterHandler importantFilter = delegate (Document document)
{
    return document.Description
    .ToLower()
    .Contains("#important");
};
```

#### We can use Statement
```c#
var testDocument = documentReader.FilterDocuments(documents, p => {
    return p.Description.ToLower().Contains("#test");
});
```
#### Or Expression lambda
```c#
var testDocument = documentReader.FilterDocuments(documents, p => p.Description.ToLower().Contains("#test"));
```
---

