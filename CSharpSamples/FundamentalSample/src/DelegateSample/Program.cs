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

var importantDocument = documentReader.FilterDocuments(documents, DocumentFilters.IsImportant);
var testDocument = documentReader.FilterDocuments(documents, DocumentFilters.IsTest);

Console.WriteLine("Important Documents:");
foreach (var document in importantDocument)
{
    Console.WriteLine($"{document.Title}\n{document.Description}\n");
}
Console.WriteLine("--------------------------------");

Console.WriteLine("Test Documents:");
foreach (var document in testDocument)
{
    Console.WriteLine($"{document.Title}\n{document.Description}\n");
}
Console.WriteLine("--------------------------------");
