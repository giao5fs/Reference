namespace DesignPattern;

public static class BridgeDesignPattern
{
    public static void Test()
    {
        IDevice tv = new TV();
        RemoteControl tvRemote = new RemoteControl(tv);

        tvRemote.TogglePower();
        tvRemote.VolumeUp();
        tvRemote.VolumeUp();
        tvRemote.VolumeDown();

        IDevice radio = new Radio();
        AdvancedRemoteControl radioRemote = new AdvancedRemoteControl(radio);

        radioRemote.TogglePower();
        radioRemote.VolumeUp();
        radioRemote.Mute();
    }
}

public interface IDevice
{
    bool IsEnabled();
    void Enable();
    void Disable();
    int GetVolume();
    void SetVolume(int volume);
}

public class TV : IDevice
{
    private bool _isEnabled;
    private int _volume;

    public bool IsEnabled()
    {
        return _isEnabled;
    }

    public void Enable()
    {
        _isEnabled = true;
        Console.WriteLine("TV is enabled");
    }

    public void Disable()
    {
        _isEnabled = false;
        Console.WriteLine("TV is disabled");
    }

    public int GetVolume()
    {
        return _volume;
    }

    public void SetVolume(int volume)
    {
        _volume = volume;
        Console.WriteLine($"TV volume set to {volume}");
    }
}

public class Radio : IDevice
{
    private bool _isEnabled;
    private int _volume;

    public bool IsEnabled()
    {
        return _isEnabled;
    }

    public void Enable()
    {
        _isEnabled = true;
        Console.WriteLine("Radio is enabled");
    }

    public void Disable()
    {
        _isEnabled = false;
        Console.WriteLine("Radio is disabled");
    }

    public int GetVolume()
    {
        return _volume;
    }

    public void SetVolume(int volume)
    {
        _volume = volume;
        Console.WriteLine($"Radio volume set to {volume}");
    }
}

public class RemoteControl
{
    protected IDevice _device;

    public RemoteControl(IDevice device)
    {
        _device = device;
    }

    public void TogglePower()
    {
        if (_device.IsEnabled())
        {
            _device.Disable();
        }
        else
        {
            _device.Enable();
        }
    }

    public void VolumeDown()
    {
        _device.SetVolume(_device.GetVolume() - 10);
    }

    public void VolumeUp()
    {
        _device.SetVolume(_device.GetVolume() + 10);
    }
}

public class AdvancedRemoteControl : RemoteControl
{
    public AdvancedRemoteControl(IDevice device) : base(device) { }

    public void Mute()
    {
        _device.SetVolume(0);
        Console.WriteLine("Volume is muted");
    }
}
