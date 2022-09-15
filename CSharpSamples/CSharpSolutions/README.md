# CSharpSolutions
Samples of solutions in C#

---

## Mapping Sample
---
## Generic Repository Sample
### steps
1. Create a Web API project.

2. Install these packages
    - Microsoft.EntityFrameworkCore.InMemory

3. Create a folder named Entities.

4. Inside Entities folder create a entity class

```C#
namespace GenericRepositorySample.Entities;
using System.ComponentModel.DataAnnotations;

public class Students
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateTime BirthDate { get; set; }
}
```

5. Create a folder named Data.

6. Inside Data folder create DataContext Class set the DbSets.
```c#
namespace GenericRepositorySample.Data;

using GenericRepositorySample.Entities;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options) 
		: base(options) {}

	// Defining DbSets
	public DbSet<Student> Students => Set<Student>();
}

```

