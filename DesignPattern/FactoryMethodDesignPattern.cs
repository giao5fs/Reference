namespace DesignPattern;

internal class FactoryMethodDesignPattern
{
    public static void Test(string[] args)
    {
        DocumentCreator pdfCreator = new PdfDocumentCreator();
        pdfCreator.PrintDocument(); // Output: Printing PDF document.

        DocumentCreator wordCreator = new WordDocumentCreator();
        wordCreator.PrintDocument(); // Output: Printing Word document.
    }
}

public interface IDocument
{
    void Print();
}

public class PdfDocument : IDocument
{
    public void Print()
    {
        Console.WriteLine("Printing PDF document.");
    }
}

public class WordDocument : IDocument
{
    public void Print()
    {
        Console.WriteLine("Printing Word document.");
    }
}

public abstract class DocumentCreator
{
    // The factory method
    public abstract IDocument CreateDocument();

    // An operation that uses the product
    public void PrintDocument()
    {
        IDocument document = CreateDocument();
        document.Print();
    }
}

public class PdfDocumentCreator : DocumentCreator
{
    public override IDocument CreateDocument()
    {
        return new PdfDocument();
    }
}

public class WordDocumentCreator : DocumentCreator
{
    public override IDocument CreateDocument()
    {
        return new WordDocument();
    }
}