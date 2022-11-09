namespace AngleSharpSample.WebApi.Models
{
    public record Student
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
