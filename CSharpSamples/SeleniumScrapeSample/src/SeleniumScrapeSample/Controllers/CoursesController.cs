using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumScrapeSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //using (IWebDriver driver = new ChromeDriver())
            //{
            //    driver.Navigate().GoToUrl("https://www.w3schools.com/");
            //};

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.w3schools.com/");


            return Ok();
        }

    }
}
