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
