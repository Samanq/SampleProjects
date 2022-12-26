# File, Directory and Path

## Getting executing path.
```C#
// Getting executing path from environment
var path = Environment.CurrentDirectory;

// Getting executing path from AppDomain
var path = AppDomain.CurrentDomain.BaseDirectory;

// Getting executing path from AppContext
var path = AppContext.BaseDirectory;

// Getting executing path from process
var path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName);
```
---

## Combining Path
```C#
// Combining Path
var path = Path.Combine(Environment.CurrentDirectory, "result.txt");
```
---

## Reading a text file
```C#
// Read text from a file
var extractedContent =  File.ReadAllText(path);
```

## Writing a text file
```C#
// Write text to a file
File.WriteAllText(path, content);
```

## Appending a text to a text file
```C#
// Append some text to a file.
File.AppendAllText(path, anotherContent);
```

