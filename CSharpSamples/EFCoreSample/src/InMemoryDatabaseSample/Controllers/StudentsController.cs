namespace InMemoryDatabaseSample.Controllers;

using InMemoryDatabaseSample.Data;
using InMemoryDatabaseSample.Entities;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudent(long id)
    {
        return Ok(await _dataContext.Students.FindAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent(Student student)
    {
        if (!ModelState.IsValid) return BadRequest();

        await _dataContext.Students.AddAsync(student);
        await _dataContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }
}
