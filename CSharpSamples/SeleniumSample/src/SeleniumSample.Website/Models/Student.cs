namespace SeleniumSample.Website.Models;

public record Student
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
}
