# CSharpSolutions
Samples of solutions in C#

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

