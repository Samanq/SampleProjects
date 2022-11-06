using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;

namespace SeleniumSample.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class BrowserController : ControllerBase
{
    private readonly IWebDriver _driver;

    public BrowserController(IWebDriver driver)
    {
        _driver = driver;
    }


    [HttpGet("ChangeBrowserSizeAndPosition")]
    public IActionResult ChangeBrowserSizeAndPosition()
    {
        _driver.Navigate().GoToUrl("http://localhost:5107/students");

        _driver.Manage().Window.Minimize();

        _driver.Manage().Window.Maximize();

        _driver.Manage().Window.Size = new System.Drawing.Size(350, 450);

        _driver.Manage().Window.Position = new System.Drawing.Point(100, 100);

        _driver.Manage().Window.FullScreen();

        return NoContent();
    }

    [HttpGet("Tabs")]
    public IActionResult GetTabs()
    {
        _driver.Navigate().GoToUrl("http://localhost:5107/");

        // Opening a new tab
        IWebElement privacyLink = _driver.FindElement(By.LinkText("Privacy"));
        privacyLink.Click();

        // Get all tabs
        var allTabs = _driver.WindowHandles;

        // Switch to second tab
        _driver.SwitchTo().Window(allTabs[1]);

        return NoContent();
    }

    [HttpGet("GetAlert")]
    public IActionResult GetAlert()
    {
        _driver.Navigate().GoToUrl("http://localhost:5107/");

        // Open an alert
        IWebElement alertLink = _driver.FindElement(By.LinkText("Open an alert"));
        alertLink.Click();

        //Thread.Sleep(2000);
        IAlert alert = _driver.SwitchTo().Alert();
        var alertText = alert.Text;

        // Close the alert
        alert.Accept();

        return Ok(alertText);
    }

    [HttpGet("Cookies")]
    public IActionResult Cookies()
    {
        _driver.Navigate().GoToUrl("http://localhost:5107/");

        // Set the cookie
        _driver.Manage().Cookies.AddCookie(new Cookie("TestId", "Cookie Value"));
        _driver.Manage().Cookies.AddCookie(new Cookie("anotherId", "true"));

        // Read the cookie
        Cookie cookie = _driver.Manage().Cookies.GetCookieNamed("TestId");

        // Delete a cookie
        _driver.Manage().Cookies.DeleteCookieNamed("anotherId");


        return Ok(cookie.Value);
    }
}
