namespace IDisposableSample.ConsoleApp;

public class FileWriteService : IDisposable
{
    private bool _disposed;

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

    public void WriteFile()
    {
        Console.WriteLine("the file has been written.");
    }
}
