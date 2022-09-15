namespace GenericRepositorySample.Entities;
using System.ComponentModel.DataAnnotations;

public class ClassRoom
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public int Number { get; set; }
}
