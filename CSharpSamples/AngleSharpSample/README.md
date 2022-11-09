# AngleSharp

## Packages
Install these packages
- AngleSharp
---
## Reading a Document
```C#
// Configuring the Browing context.
using var context = BrowsingContext.New(Configuration.Default);
// Loading the document
using var doc = await context.OpenAsync(req => req.Content("<html></html>"));
```

## Get elements by tag name
```C#
var liElements = doc.QuerySelectorAll("li");
// OR
var allDivs = doc.GetElementsByTagName("div");
```

## Get elements by class name
```C#
var studentCards = doc.GetElementsByClassName("student-card");
```

## Get elements by css selector
```C#
// Get single element
var submitButton = doc.QuerySelector("a.submit");
// Get all elements
var cards = doc.QuerySelectorAll("div.student-card");
```