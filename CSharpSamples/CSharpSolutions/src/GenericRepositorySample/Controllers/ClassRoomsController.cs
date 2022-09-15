using GenericRepositorySample.Entities;
using GenericRepositorySample.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GenericRepositorySample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassRoomsController : ControllerBase
{
    private readonly IClassRoomRepository _repository;

    // Injection the Repository
    public ClassRoomsController(IClassRoomRepository classRoomRepository)
    {
        _repository = classRoomRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetClassRooms()
    {
        return Ok(await _repository.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClassRoom(long id)
    {
        return Ok(await _repository.Get(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateClassRoom(ClassRoom classRoom)
    {
        if (!ModelState.IsValid) return BadRequest();

        await _repository.Add(classRoom);

        return CreatedAtAction(nameof(GetClassRoom), new { id = classRoom.Id }, classRoom);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit([FromBody] ClassRoom classRoom, long id)
    {
        if (id != classRoom.Id) return BadRequest();

        await _repository.Edit(classRoom);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _repository.Delete(id);

        return Ok();
    }
}
