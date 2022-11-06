# Selenium Scrape Sample
## Context
---
## Selenium Installation

Install these packages from NuGet

- Selenium.WebDriver
- Selenium.WebDriver.ChromeDriver (The version should be compatible with your chrome on your machine)
- Selenium.Support
---
## Initializing the WebDriver
I'm using the using keyword for disposing the WebDriver in the end.
```C#
using (IWebDriver driver = new ChromeDriver())
{
    
};
```

## Navigating to a URL
```C#
driver.Navigate().GoToUrl("https://www.w3schools.com/");
```

## Getting Page Title
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");
string pageTitle = driver.Title;
```

## Getting Page URL
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");
string url = driver.Url;
```

## Refresh Page
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");
driver.Navigate().Refresh();
```

## Going Back and Forward
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");
driver.Navigate().GoToUrl("https://www.w3schools.com/about/default.asp");
driver.Navigate().Back();
driver.Navigate().Forward();
```

## Getting and element by tag
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");

IWebElement firstLink = driver.FindElement(By.TagName("a"));
```

## Getting and element by Id
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");

IWebElement acceptCookiesButton = 
    driver.FindElement(By.Id("accept-choices"));

string acceptCookiesButtonText = acceptCookiesButton.Text;
```

## Getting and element by class name
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");

IWebElement greenDiv =
    driver.FindElement(By.ClassName("ws-light-green"));
```

## Getting an element by link text
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");

IWebElement whereToBeginButton = 
    driver.FindElement(By.LinkText("Not Sure Where To Begin?"));
```

## Getting an element by partial link text
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");

IWebElement shopLink =
    driver.FindElement(By.PartialLinkText("- shop now!"));
```

## Getting an element by CSS Selector
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");

IWebElement startFreeButton =
    driver.FindElement(By.CssSelector("a.ws-btn.tut-button"));
var startFreeButtonText = startFreeButton.Text;
```

## Getting an element by XPath
For finding XPath you can use this tool http://xpather.com/
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");

IWebElement heading = 
    driver.FindElement(By.XPath("/html/body/div[5]/div[1]/div/h1"));
```

## Getting and element's attribute 
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");

IWebElement emailBox =
    driver.FindElement(By.Id("email-box"));
string emailBoxName = emailBox.GetAttribute("name");
```

## Getting a Select element
```C#
IWebDriver _driver = new ChromeDriver()
driver.Navigate().GoToUrl("http://localhost:5107/students/create");

SelectElement genderSelect =
    new SelectElement(_driver.FindElement(By.Id("gender")));
var genders = genderSelect.Options;
```
---

## Getting an option inside a select
```C#
// Getting a Select element
// We Need Selenium.Support package for SelectElement
SelectElement genderSelect =
    new SelectElement(_driver.FindElement(By.Id("gender")));
var genders = genderSelect.Options;

// Selecting an option inside a select
// We can select by index, value and text
genderSelect.SelectByValue("Male");
```
## Click on an element
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");

IWebElement acceptCookiesButton = 
    driver.FindElement(By.Id("accept-choices"));

acceptCookiesButton.Click();
```

## Submitting a form
```C#
// We can either select the form element or any element within the form.
IWebElement form =
    _driver.FindElement(By.Id("student-from"));
form.Submit();
```

## Wait for an element
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");

WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
IWebElement submitLink =
    wait.Until(d => d.FindElement(By.Id("submit")));
```

## Implicit wait
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");

driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
```

## Get Multiple Elements
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");

IReadOnlyCollection<IWebElement> rows = 
    driver.FindElements(By.TagName("tr"));
```

## Get and close an alert
```C#
_driver.Navigate().GoToUrl("http://localhost:5107/");

// Open an alert
IWebElement alertLink = _driver.FindElement(By.LinkText("Open an alert"));
alertLink.Click();

// Switching to the alert
IAlert alert = _driver.SwitchTo().Alert();

// Getting the alert text
var alertText = alert.Text;

// Close the alert
alert.Accept();

// Dismiss the alert
alert.Dismiss();
```

## Sending keys to an element
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");

IWebElement searchBox =
    driver.FindElement(By.Id("search-box"));
searchBox.SendKeys("python course");
```

## Get and switch Tabs
```C#
_driver.Navigate().GoToUrl("http://localhost:5107/");

// Opening a new tab
IWebElement privacyLink = _driver.FindElement(By.LinkText("Privacy"));
privacyLink.Click();

// Get all tabs
var allTabs = _driver.WindowHandles;

// Switch to second tab
_driver.SwitchTo().Window(allTabs[1]);
``` 

## Get and set cookies
```C#
_driver.Navigate().GoToUrl("http://localhost:5107/");

// Set cookies
_driver.Manage().Cookies.AddCookie(new Cookie("TestId", "Cookie Value"));

// Read the cookie
Cookie cookie = _driver.Manage().Cookies.GetCookieNamed("TestId");
var cookieValue = cookie.Value;

// Delete a cookie
_driver.Manage().Cookies.DeleteCookieNamed("TestId");
```


## Changing browser aize and position
```C#
IWebDriver _driver = new ChromeDriver()
_driver.Navigate().GoToUrl("http://localhost:5107/students");

// Minimize the browser
_driver.Manage().Window.Minimize();

// Maximize the browser
_driver.Manage().Window.Maximize();

// Chnage the browser size
_driver.Manage().Window.Size = new System.Drawing.Size(350, 450);

// Chnage the browser position
_driver.Manage().Window.Position = new System.Drawing.Point(100, 100);

// Full screen the browser
_driver.Manage().Window.FullScreen();
```



## Tips
- Never minimize the browser before navigate.



