namespace GenericRepositorySample.Data;

using GenericRepositorySample.Entities;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options) 
		: base(options) {}

	// Defining DbSets
	public DbSet<Student> Students => Set<Student>();
    public DbSet<ClassRoom> ClassRooms => Set<ClassRoom>();
}
