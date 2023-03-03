using System.ComponentModel.DataAnnotations;

namespace AuditTrailSample.Domain.Entities;

public abstract class BaseEntity
{
    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public DateTime ModifiedDate { get; set; }
}
