using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumSample.Api.Models;

namespace SeleniumSample.Api.Controllers;

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

    [HttpPost]
    public IActionResult Post([FromBody]Student student)
    {
        // Navigate to an url
        _driver.Navigate().GoToUrl("http://localhost:5107/students/create");

        // Getting an element by Id
        IWebElement nameBox = _driver.FindElement(By.Id("name"));
        // Sending keys to an element
        nameBox.SendKeys(student.Name);

        // Getting a Select element
        // We Need Selenium.Support package for SelectElement
        SelectElement genderSelect =
            new SelectElement(_driver.FindElement(By.Id("gender")));
        var genders = genderSelect.Options;

        // Selecting an option inside a select
        // We can select by index, value and text
        genderSelect.SelectByValue(student.Gender);

        // Submitting a form
        // We can either select the form element or any element within the form.
        IWebElement form = _driver.FindElement(By.Id("student-from"));
        form.Submit();

        return Ok();
    }
}
