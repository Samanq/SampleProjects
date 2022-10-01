public class DocumentReader
{
    public delegate bool DocumentFilterHandler(Document document);

    public List<Document> FilterDocuments(IEnumerable<Document> documents, DocumentFilterHandler filter)
    {
        List<Document> filteredDocuments = new List<Document>();

        foreach (var document in documents)
        {
            if (filter(document))
            {
                filteredDocuments.Add(document);
            }
        }

        return filteredDocuments;
    }
}
