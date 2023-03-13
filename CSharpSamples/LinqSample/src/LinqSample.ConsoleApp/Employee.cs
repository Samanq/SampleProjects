namespace LinqSample.ConsoleApp;

public record Employee
{
    public long Id { get; set; }
    public long EmployeeTypeId { get; set; }
    public string Name { get; set; } = string.Empty;
}
