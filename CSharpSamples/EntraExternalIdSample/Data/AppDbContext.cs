using Microsoft.EntityFrameworkCore;

namespace EntraExternalIdSample.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> Users => Set<AppUser>();
}

/// One row per external identity. Keyed by the Entra object id ("oid" claim), which never changes for a user.
public class AppUser
{
    public int Id { get; set; }
    public required string ObjectId { get; set; }
    public string? Email { get; set; }
    public string? DisplayName { get; set; }
    public DateTime FirstSeenUtc { get; set; }
    public DateTime LastSeenUtc { get; set; }
    public int LoginCount { get; set; }
}
