namespace IDisposableSample.ConsoleApp;

public class PdfWriteService : FileWriteService
{
    protected override void Dispose(bool disposing)
    {
        Console.WriteLine($"Disposing {nameof(PdfWriteService)}");

        base.Dispose(disposing);
    }
}
