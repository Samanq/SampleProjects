public static class DocumentFilters
{
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
