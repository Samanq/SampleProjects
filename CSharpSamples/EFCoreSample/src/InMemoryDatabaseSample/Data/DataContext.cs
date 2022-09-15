namespace InMemoryDatabaseSample.Data;

using InMemoryDatabaseSample.Entities;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) {}

    // Definign DbSets
    public DbSet<Student> Students => Set<Student>();
}
