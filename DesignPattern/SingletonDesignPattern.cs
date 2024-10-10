namespace DesignPattern;

public sealed class SingletonDesignPattern
{

}
public sealed class SingletonBasic
{
    private static readonly SingletonBasic _instance = new SingletonBasic();

    // Private constructor to prevent instantiation from other classes
    private SingletonBasic() { }

    // Public static property to get the singleton instance
    public static SingletonBasic GetInstance()
    {
        return _instance;
    }

    // Example method
    public void DoSomething()
    {
        Console.WriteLine("Singleton instance is doing something.");
    }
}

public sealed class SingletonUseLock
{
    private static SingletonUseLock? _instance;
    private static readonly object _lock = new object();

    // Private constructor to prevent instantiation from other classes
    private SingletonUseLock() { }

    // Public static property to get the singleton instance
    public static SingletonUseLock GetInstance()
    {
        // Double-checked locking
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new SingletonUseLock();
                }
            }
        }
        return _instance;
    }

    // Example method
    public void DoSomething()
    {
        Console.WriteLine("Singleton instance is doing something.");
    }
}

public sealed class SingletonLazy
{
    private static readonly Lazy<SingletonLazy> _lazyInstance = new(() => new SingletonLazy());

    // Private constructor to prevent instantiation from other classes
    private SingletonLazy() { }

    // Public static property to get the singleton instance
    public static SingletonLazy GetInstance()
    {
        return _lazyInstance.Value;
    }

    // Example method
    public void DoSomething()
    {
        Console.WriteLine("Singleton instance is doing something.");
    }
}


class Program
{
    static void Main(string[] args)
    {
        SingletonBasic singleton = SingletonBasic.GetInstance();
        singleton.DoSomething();

        SingletonLazy singletonLazy = SingletonLazy.GetInstance();
        singleton.DoSomething();

        SingletonUseLock singleton1 = SingletonUseLock.GetInstance();
        singleton1.DoSomething();

        SingletonUseLock singleton2 = SingletonUseLock.GetInstance();
        singleton2.DoSomething();

        Console.WriteLine(Object.ReferenceEquals(singleton1, singleton2)); // Outputs: True
    }
}

