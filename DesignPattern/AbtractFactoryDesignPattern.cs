namespace DesignPattern;

public static class AbtractFactoryDesignPattern
{
    public static void Test()
    {
        IGUIFactory factory;

        // Here you can decide which factory to use, e.g., based on configuration or runtime information.
        string osType = "Windows"; // This can come from a configuration file or environment variable

        if (osType == "Windows")
        {
            factory = new WinFactory();
        }
        else if (osType == "Mac")
        {
            factory = new MacFactory();
        }
        else
        {
            factory = new IBMFactory();
        }

        Application app = new Application(factory);
        app.Render();
    }
}

public interface IButton
{
    void Render();
}

public interface ICheckbox
{
    void Render();
}

public interface ITextBox
{
    void Render();
}

public class WinButton : IButton
{
    public void Render()
    {
        Console.WriteLine("Rendering a Windows button.");
    }
}

public class MacButton : IButton
{
    public void Render()
    {
        Console.WriteLine("Rendering a Mac button.");
    }
}

public class IBMButton : IButton
{
    public void Render()
    {
        Console.WriteLine("Rendering a IBM button.");
    }
}

public class WinCheckbox : ICheckbox
{
    public void Render()
    {
        Console.WriteLine("Rendering a Windows checkbox.");
    }
}

public class MacCheckbox : ICheckbox
{
    public void Render()
    {
        Console.WriteLine("Rendering a Mac checkbox.");
    }
}

public class IBMCheckbox : ICheckbox
{
    public void Render()
    {
        Console.WriteLine("Rendering a IBM checkbox.");
    }
}


public class WinTextBox : ITextBox
{
    public void Render()
    {
        Console.WriteLine("Rendering a Windows textbox.");
    }
}

public class MacTextBox : ITextBox
{
    public void Render()
    {
        Console.WriteLine("Rendering a Mac textbox.");
    }
}

public class IBMTextBox : ITextBox
{
    public void Render()
    {
        Console.WriteLine("Rendering a IBM textbox.");
    }
}

public interface IGUIFactory
{
    IButton CreateButton();
    ICheckbox CreateCheckbox();
    ITextBox CreateTextbox();
}

public class WinFactory : IGUIFactory
{
    public IButton CreateButton()
    {
        return new WinButton();
    }

    public ICheckbox CreateCheckbox()
    {
        return new WinCheckbox();
    }

    public ITextBox CreateTextbox()
    {
        return new WinTextBox();
    }
}

public class MacFactory : IGUIFactory
{
    public IButton CreateButton()
    {
        return new MacButton();
    }

    public ICheckbox CreateCheckbox()
    {
        return new MacCheckbox();
    }

    public ITextBox CreateTextbox()
    {
        return new MacTextBox();
    }
}

public class IBMFactory : IGUIFactory
{
    public IButton CreateButton()
    {
        return new IBMButton();
    }

    public ICheckbox CreateCheckbox()
    {
        return new IBMCheckbox();
    }

    public ITextBox CreateTextbox()
    {
        return new IBMTextBox();
    }
}

public class Application
{
    private readonly IButton _button;
    private readonly ICheckbox _checkbox;
    private readonly ITextBox _textbox;

    public Application(IGUIFactory factory)
    {
        _button = factory.CreateButton();
        _checkbox = factory.CreateCheckbox();
        _textbox = factory.CreateTextbox();
    }

    public void Render()
    {
        _button.Render();
        _checkbox.Render();
        _textbox.Render();
    }
}