using IDisposableSample.ConsoleApp;


//using (FileWriteService fileWriteService = new FileWriteService())
//{
//    fileWriteService.WriteFile();
//}

using (PdfWriteService pdfWriteService = new PdfWriteService())
{
    pdfWriteService.WriteFile();
}



Console.ReadLine();