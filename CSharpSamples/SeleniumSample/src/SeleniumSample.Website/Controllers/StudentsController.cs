using Microsoft.AspNetCore.Mvc;
using SeleniumSample.Website.Models;

namespace SeleniumSample.Website.Controllers
{
    public class StudentsController : Controller
    {
        private static List<Student> _students = new List<Student>
        {
            new Student
            {
                Id = 1,
                Name = "John",
                Gender = "Male"
            },
            new Student
            {
                Id = 2,
                Name = "Jane",
                Gender = "Female"
            },
            new Student
            {
                Id = 3,
                Name = "Peter",
                Gender = "Rather not to say"
            },
        };
        public IActionResult Index()
        {
            return View(_students);
        }

        public IActionResult Create()
        {
            return View(new Student());
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid) _students.Add(student);
            return RedirectToAction("Index");
        }
    }
}
