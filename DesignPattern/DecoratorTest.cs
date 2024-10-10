namespace DesignPattern;

public static class DecoratorTest
{
    public static void Test()
    {
        INotification emailNotification = new EmailNotification();
        INotification smsNotification = new SMSNotification();

        LoggedNotification loggedEmail = new(emailNotification);
        LoggedNotification loggedSMS = new(smsNotification);

        CombinedNotification combinedNotification = new CombinedNotification(smsNotification);
        combinedNotification.Send("DDDD");

        loggedSMS.Send("FFF");
        loggedEmail.Send("FFF");
    }
}

public abstract class DecoratedNotification
{
    public INotification _notification;

    protected DecoratedNotification(INotification notification)
    {
        _notification = notification;
    }

    public abstract void Send(string message);
}

public class LoggedNotification : DecoratedNotification
{
    public LoggedNotification(INotification notification) : base(notification)
    {
    }

    public override void Send(string message)
    {
        Console.WriteLine($"Logging... {_notification.GetType()}");
        _notification.Send(message);
    }
}

public class CombinedNotification : DecoratedNotification
{
    public CombinedNotification(INotification notification) : base(notification)
    {
    }

    public override void Send(string message)
    {
        Console.WriteLine($"Sending.. {message}");
        _notification.Send(message);
    }
}

public interface INotification
{
    void Send(string message);
}

public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"Email:{message}");
    }
}

public class SMSNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"SMS:{message}");
    }
}