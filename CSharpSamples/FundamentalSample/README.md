# C# Fundamentals

## Delegate
- Delegate is a <ins>type</ins> that holds a <ins>reference</ins> to a <ins>method</ins>.
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
var importantDocument = documentReader.FilterDocuments(documents, DocumentFilters.IsImportant);

Console.WriteLine("Important Documents:");
foreach (var document in importantDocument)
{
    Console.WriteLine($"{document.Title}\n{document.Description}\n");
}
```

---
## Anonymous Method
- Anonymous method is a method <ins>without a name</ins>.
- Anonumous method can be defined by delegate keyword and can be assigned to a variable of type delegate.
---