using AuditTrailSample.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuditTrailSample.Infrastructure.Persistence.DataContexts;

public class AuditTrailSampleDb : DbContext
{
    public AuditTrailSampleDb(DbContextOptions<AuditTrailSampleDb> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Audit> Audits { get; set; }



    // Overriding SaveChanges to add our logic before saving changes.
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var modifiedEntities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added)
            .ToList();

        foreach (var entity in modifiedEntities)
        {
            var entityName = entity.Entity.GetType().Name;
            var oldValue = string.Empty;
            var newValue = string.Empty;


            if (entity.State == EntityState.Modified)
            {
                // In EF6 you can use entity.OriginalValues.PropertyNames
                foreach (var property in entity.OriginalValues.Properties)
                {
                    var originalValue = entity.OriginalValues[property.Name]?.ToString();
                    var currentValue = entity.CurrentValues[property.Name]?.ToString();

                    if (originalValue != currentValue)
                    {
                        oldValue += $"{property.Name}: {originalValue}, ";
                        newValue += $"{property.Name}: {currentValue}, ";
                    }
                }
            }
            else if (entity.State == EntityState.Added)
            {
                foreach (var property in entity.CurrentValues.Properties)
                {
                    var currentValue = entity.CurrentValues.GetValue<object>(property.Name)?.ToString();
                    newValue += $"{property.Name}: {currentValue}, ";
                }
            }

            var audit = new Audit
            {
                EntityName = entityName,
                OldValue = oldValue.TrimEnd(',', ' '),
                NewValue = newValue.TrimEnd(',', ' '),
                Username = "Admin",
                AuditType = "Unknown",
                DateTime = DateTime.UtcNow
            };

            Audits.Add(audit);
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
