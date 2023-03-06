using AuditTrailSample.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AuditTrailSample.Infrastructure.Persistence.DataContexts;

public class AuditTrailSampleDb : DbContext
{
    public AuditTrailSampleDb(DbContextOptions<AuditTrailSampleDb> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Audit> Audits { get; set; }


    // Overriding SaveChanges to add our logic before saving changes.
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var affectedEntities = ChangeTracker.Entries()
            .Where(e =>
                e.State == EntityState.Modified ||
                e.State == EntityState.Added ||
                e.State == EntityState.Deleted)
            .ToList();

        foreach (var entity in affectedEntities)
        {
            switch (entity.State)
            {
                case EntityState.Added:
                    // In EF6 you can use entity.OriginalValues.PropertyNames
                    var tempId = entity.CurrentValues["Id"]?.ToString();

                    foreach (var property in entity.CurrentValues.Properties)
                    {
                        string? originalValue = null;
                        var currentValue = entity.CurrentValues[property.Name]?.ToString();

                        AddProperty(entity, property, originalValue, currentValue);
                    }
                    await base.SaveChangesAsync(cancellationToken);
                    FixIdAfterInsertion(affectedEntities, tempId);
                    break;
                case EntityState.Modified:
                    foreach (var property in entity.OriginalValues.Properties)
                    {
                        var originalValue = entity.OriginalValues[property.Name]?.ToString();
                        var currentValue = entity.CurrentValues[property.Name]?.ToString();

                        if (originalValue != currentValue)
                        {
                            AddProperty(entity, property, originalValue, currentValue);
                        }
                    }
                    break;
                case EntityState.Deleted:
                    foreach (var property in entity.OriginalValues.Properties)
                    {
                        var originalValue = entity.OriginalValues[property.Name]?.ToString();
                        string? currentValue = null;

                        AddProperty(entity, property, originalValue, currentValue);
                    }
                    break;
                default:
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    private void FixIdAfterInsertion(IEnumerable<EntityEntry> affectedEntities, string? currentId)
    {
        foreach (var savedEntity in affectedEntities)
        {
            if (savedEntity.State == EntityState.Unchanged)
            {
                var entityName = savedEntity.Entity.GetType().Name;
                var entityId = savedEntity.Property("Id").CurrentValue;
                var currentAudit = Audits
                    .Where(a => a.NewValue == currentId)
                    .SingleOrDefault();

                if (currentAudit != null)
                {
                    currentAudit.NewValue = entityId?.ToString();
                    Entry(currentAudit).State = EntityState.Modified;
                }
            }
        }
    }

    private void AddProperty(EntityEntry entity, IProperty property, string? originalValue, string? currentValue)
    {
        var entityName = entity.Entity.GetType().Name;
        var entityState = entity.State.ToString();

        var audit = new Audit
        {
            EntityName = entityName,
            PropertyName = property.Name,
            OldValue = originalValue,
            NewValue = currentValue,
            Username = "Admin",
            AuditType = entityState,
            DateTime = DateTime.UtcNow
        };

        Audits.Add(audit);
    }
}
