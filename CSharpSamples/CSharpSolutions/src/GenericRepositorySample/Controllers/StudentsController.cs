namespace GenericRepositorySample.Controllers;

using GenericRepositorySample.Entities;
using GenericRepositorySample.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IGenericRepository<Student> _repository;

    // Injecting the repository into the controller.
    public StudentsController(IGenericRepository<Student> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        return Ok(await _repository.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudent(long id)
    {
        return Ok(await _repository.Get(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent(Student student)
    {
        if (!ModelState.IsValid) return BadRequest();

        await _repository.Add(student);

        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit([FromBody] Student student, long id)
    {
        if (id != student.Id) return BadRequest();

        await _repository.Edit(student);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _repository.Delete(id);

        return Ok();
    }
}
