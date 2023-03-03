using AuditTrailSample.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuditTrailSample.Infrastructure.Persistence.DataContexts;

public class AuditTrailSampleDb : DbContext
{
    public AuditTrailSampleDb(DbContextOptions<AuditTrailSampleDb> options) : base(options) { }

    public DbSet<Product> Products { get; set; }


    // Overriding SaveChanges to add our logic before saving changes.
    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
}
