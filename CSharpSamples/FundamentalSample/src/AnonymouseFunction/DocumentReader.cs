
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

