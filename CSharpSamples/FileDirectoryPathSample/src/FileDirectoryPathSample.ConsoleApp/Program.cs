using System.Diagnostics;

string content = @"There are some text here for example.
you can write this content to a file + another texts";

string anotherContent = @"
-
Anothe text for content,";

Console.WriteLine("Hello, World!");

// Getting executing path.
Console.WriteLine($"Environment.CurrentDirectory : {Environment.CurrentDirectory}");
Console.WriteLine($"AppDomain.CurrentDomain.BaseDirectory : {AppDomain.CurrentDomain.BaseDirectory}");
Console.WriteLine($"AppContext.BaseDirectory : {AppContext.BaseDirectory}");
Console.WriteLine($"Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName : {Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName)}");

// Combining Path
var path = Path.Combine(Environment.CurrentDirectory, "result.txt");

// Write text to a file
File.WriteAllText(path, content);

// Append some text to a file.
File.AppendAllText(path, anotherContent);

// Read text from a file
var extractedContent =  File.ReadAllText(path);
Console.WriteLine(extractedContent);
