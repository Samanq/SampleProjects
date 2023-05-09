# IDisposable Sample
In C#, **IDisposable** is an **interface** that provides a mechanism for <u>releasing unmanaged resources explicitly.</u> It is commonly used to clean up resources such as file handles, database connections, network connections, and other system resources that are not automatically managed by the .NET runtime.

The IDisposable interface defines a single method called **Dispose()**, which is responsible for releasing the resources held by an object. It is up to the implementer of the interface to determine how resources should be released in the Dispose() method.

---

## Using IDisposable Interface
For implementing the IDisposable interface we should just use **:** and Implement the Dispose method.
```C#
public class FileWriteService : IDisposable
{
    public void Dispose()
    {
        Console.WriteLine($"Disposing {nameof(FileWriteService)}");
    }

    public void WriteFile()
    {
        Console.WriteLine("the file has been written.");
    }
}
```
The Dispose method will be called at the end of the using.
```C#
using (FileWriteService fileWriteService = new FileWriteService())
{
   fileWriteService.WriteFile();
}
```
Result : 
```Powershell
the file has been written.
Disposing FileWriteService
```

---

## Overriding the Dispose method

If we override the dispose method like we do in this sample, you can see the Dispose method in the base class will be called, not the Dispose method in the inherited class.
```C#
public class PdfWriteService : FileWriteService
{
    // We used "new" keyword to hide the base member intentionally. 
    public new void Dispose()
    {
        Console.WriteLine($"Disposing {nameof(PdfWriteService)}");
    }
}
``` 
```C#
using (PdfWriteService pdfWriteService = new PdfWriteService())
{
    pdfWriteService.WriteFile();
}
```
Result :
```Powershell
the file has been written.
Disposing FileWriteService
```
For fixing this issue we can have another Dispose method with a bool value in Base class and in the Dispose method call this new Dispose method.
```C#
public class FileWriteService : IDisposable
{
    private bool _disposed;

    public FileWriteService(bool disposed)
    {
        _disposed = disposed;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }
        if (disposing)
        {
            // TODO: dispose managed state (managed objects).
        }
        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.
        Console.WriteLine($"Disposing {nameof(FileWriteService)}");

        _disposed = true;
    }

    public void Dispose()
    {
        // Dispose of unmanaged resources.
        Dispose(true);

        // Suppress finalization.
        GC.SuppressFinalize(this);
    }
}

```
For mor information you can check [Implement a Dispose method
](https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose)