namespace SeleniumSample.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using SeleniumSample.Api.Models;

[Route("[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IWebDriver _driver;
    public StudentsController(IWebDriver driver)
    {
        _driver = driver;
    }

    [HttpGet]
    public IActionResult GetStudents()
    {
        // Navigate to an url
        _driver.Navigate().GoToUrl("http://localhost:5107/students");
        
        // Find elements by class name
        IReadOnlyCollection<IWebElement> elements =
            _driver.FindElements(By.ClassName("student-card"));

        var students = elements.Select(s => new Student 
        {
            // Find an element by Id
            Id = long.Parse(s.FindElement(By.Id("id")).Text),
            // Find an element by Name
            Name = s.FindElement(By.Name("Name")).Text,
            // Find an element by tag
            Gender = s.FindElement(By.TagName("strong")).Text
        });

        return Ok(students);
    }
}
