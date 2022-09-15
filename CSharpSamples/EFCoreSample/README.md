# EFCore Sample
Using Web API with .Net 6

---

## In Memory Database sample
### Steps
1. Create WebAPI project.
2. Install these packages
    - Microsoft.EntityFrameworkCore.InMemory
3. Create a folder and name it Entities.
4. Create a entity class inside Entities folder.
```c#
namespace InMemoryDatabaseSample.Entities;
using System.ComponentModel.DataAnnotations;

public class Student
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
5. Create a folder name Data.
6. Inside the data folder create DataContext class.
7. Define the DBSets.
```C#
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
```

8. Register the DbContext Service in program.cs by adding this line.
```C#
...
// Registering DbContext
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("InMemoryDatabaseSampleDb"));
...
```

9. Create a Controller inside Controllers folder.
10. Inject the DataContex into the controller. (Not a good practice)
```C#
namespace InMemoryDatabaseSample.Controllers;

using InMemoryDatabaseSample.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly DataContext _dataContext;

    // Injection the DataContext
    public StudentsController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        return Ok(await _dataContext.Students.ToListAsync());
    }
}

```
