namespace ApiValidationSample.Models;

public class Dog
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Color { get; set; } = string.Empty;
    public string OwnerEmail { get; set; } = string.Empty;
    public virtual ICollection<DogToy> DogToys { get; set; } = new HashSet<DogToy>();
}
