using static DocumentReader;

var documents = new List<Document>
{
    new Document
    {
        Title = "First Test",
        Description = "Some data with test keyword #test"
    },
    new Document
    {
        Title = "Real Data",
        Description = "There is real data here. #important"
    },
    new Document
    {
        Title = "Some checks",
        Description = "this description is a test. #test"
    },
    new Document
    {
        Title = "Another Example",
        Description = "This is not a important document and it's just a test. #test"
    },
    new Document
    {
        Title = "Importand Data",
        Description = "We have very important data here #important"
    },
    new Document
    {
        Title = "Some other data",
        Description = "We have data here #archive"
    }
};

DocumentReader documentReader = new DocumentReader();

// This is a anonymouse function.
// It doesn't have a name and it defines by delegate keyword
DocumentFilterHandler importantFilter = delegate (Document document)
{
    return document.Description
    .ToLower()
    .Contains("#important");
};

// documents parameter is a list of documents we created earlier.
// importantFilter is a anonyumouse method.
var importantDocuments = documentReader.FilterDocuments(documents, importantFilter);

Console.WriteLine("Important Documents:");
foreach (var document in importantDocuments)
{
    Console.WriteLine($"{document.Title}\n{document.Description}\n");
}
Console.WriteLine("--------------------------------");


// Using lambda expression for anonymouse function
var testDocuments = documentReader.FilterDocuments(documents, p => p.Description.ToLower().Contains("#test"));

Console.WriteLine("Test Documents:");
foreach (var document in testDocuments)
{
    Console.WriteLine($"{document.Title}\n{document.Description}\n");
}
Console.WriteLine("--------------------------------");