namespace DesignPattern;

public static class CompositeDesignPattern
{
    public static void Test()
    {
        IFileSystemComponent file1 = new File("File1.txt");
        IFileSystemComponent file2 = new File("File2.txt");
        IFileSystemComponent file3 = new File("File3.txt");
        IFileSystemComponent file4 = new File("File4.txt");
        IFileSystemComponent file5 = new File("File5.txt");

        // Create directories
        Directory directory1 = new Directory("Directory1");
        Directory directory2 = new Directory("Directory2");
        Directory directory3 = new Directory("Directory3");
        Directory rootDirectory = new Directory("Root");

        // Build the tree structure
        directory1.Add(file1);
        directory1.Add(file2);
        directory1.Add(file3);
        directory1.Add(file4);
        directory1.Add(file5);

        directory2.Add(file3);

        directory3.Add(file4);
        directory3.Add(file5);

        directory2.Add(directory3);

        rootDirectory.Add(directory1);
        rootDirectory.Add(directory2);

        // Display the file system
        rootDirectory.Display(11);
    }
}

public interface IFileSystemComponent
{
    void Display(int depth);
}

public class File : IFileSystemComponent
{
    private string _name;

    public File(string name)
    {
        _name = name;
    }

    public void Display(int depth)
    {
        Console.WriteLine(new String('-', depth) + _name);
    }
}


public class Directory : IFileSystemComponent
{
    private string _name;
    private List<IFileSystemComponent> _children = new List<IFileSystemComponent>();

    public Directory(string name)
    {
        _name = name;
    }

    public void Add(IFileSystemComponent component)
    {
        _children.Add(component);
    }

    public void Remove(IFileSystemComponent component)
    {
        _children.Remove(component);
    }

    public void Display(int depth)
    {
        Console.WriteLine(new String('-', depth) + _name);
        foreach (var component in _children)
        {
            component.Display(depth + 2);
        }
    }
}
