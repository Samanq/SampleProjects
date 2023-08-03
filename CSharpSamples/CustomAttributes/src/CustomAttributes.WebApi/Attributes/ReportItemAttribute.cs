namespace CustomAttributes.WebApi.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ReportItemAttribute : Attribute
{
    public ReportItemAttribute(string title)
    {
        Title = title;
    }
    
    public string Title { get; }

    public string Currency { get; set; } = string.Empty;
}
