using System.ComponentModel.DataAnnotations;

namespace AuditTrailSample.Domain.Entities;

public class Audit
{
    public long Id { get; set; }

    [Required]
    public string EntityName { get; set; } = string.Empty;

    [Required]
    public string PropertyName { get; set; } = string.Empty;

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string AuditType { get; set; } = string.Empty;

    [Required]
    public DateTime DateTime { get; set; }
}
