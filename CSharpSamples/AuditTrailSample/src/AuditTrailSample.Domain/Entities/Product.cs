using System.ComponentModel.DataAnnotations;

namespace AuditTrailSample.Domain.Entities;

public class Product
{
    public long Id { get; set; }

    [Required]
    public string Title {get; set; } = null!;

    [Required]
    public double Price { get; set; }
}
