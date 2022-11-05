# Selenium Scrape Sample
## Context
---
## Selenium Installation

Install these packages from NuGet

- Selenium.WebDriver
- Selenium.WebDriver.ChromeDriver (The version should be compatible with your chrome on your machine)
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

## Click on an element
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");

IWebElement acceptCookiesButton = 
    driver.FindElement(By.Id("accept-choices"));

acceptCookiesButton.Click();
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

## Sending keys to an element
```C#
IWebDriver driver = new ChromeDriver()
driver.Navigate().GoToUrl("https://www.w3schools.com/");

IWebElement searchBox =
    driver.FindElement(By.Id("search-box"));
searchBox.SendKeys("python course");
```




            


