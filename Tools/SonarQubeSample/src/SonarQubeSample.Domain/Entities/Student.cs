namespace SonarQubeSample.Domain.Entities;

public record Student
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
