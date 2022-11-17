namespace ApiValidationSample.Models;

public record DogToy
{
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public bool HasSound { get; set; }
}
