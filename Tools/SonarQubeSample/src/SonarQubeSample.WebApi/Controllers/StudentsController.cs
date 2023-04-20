using Microsoft.AspNetCore.Mvc;
using SonarQubeSample.Domain.Entities;
using SonarQubeSample.Infrastructure.Persistent;

namespace SonarQubeSample.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository _studentRepository;

        public StudentsController()
        {
            _studentRepository = new StudentRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var students = _studentRepository.GetAll();

            return Ok(students);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(long id)
        {
            var student = _studentRepository.GetById(id);

            return Ok(student);
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            _studentRepository.Create(student);
            return Ok();
        }

        [HttpPut]
        public IActionResult Edit(long id, Student student)
        {
            _studentRepository.Edit(id, student);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            _studentRepository.Delete(id);

            return Ok();
        }

        string username = "John@Sample.com";
        string password = "password";
    }
}
