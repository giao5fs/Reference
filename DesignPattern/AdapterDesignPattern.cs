namespace DesignPattern;

// Step 5: Use
public static class AdapterDesignPattern
{
    public static void Test()
    {
        AudioPlayer audioPlayer = new AudioPlayer();

        audioPlayer.Play("mp3", "beyond the horizon.mp3");
        audioPlayer.Play("mp4", "alone.mp4");
        audioPlayer.Play("vlc", "far far away.vlc");
        audioPlayer.Play("avi", "mind me.avi");
    }
}


// Step 1: Define the Target Interface
public interface IMediaPlayer
{
    void Play(string audioType, string fileName);
}

// Step 2: Implement the Adaptee Class
public class AdvancedMediaPlayer
{
    public void PlayVlc(string fileName)
    {
        Console.WriteLine("Playing vlc file. Name: " + fileName);
    }

    public void PlayMp4(string fileName)
    {
        Console.WriteLine("Playing mp4 file. Name: " + fileName);
    }
}

// Step 3: Implement the Adapter Class
public class MediaAdapter : IMediaPlayer
{
    private AdvancedMediaPlayer _advancedMusicPlayer;

    public MediaAdapter()
    {
        _advancedMusicPlayer = new AdvancedMediaPlayer();
    }

    public void Play(string audioType, string fileName)
    {
        if (audioType.Equals("vlc", StringComparison.OrdinalIgnoreCase))
        {
            _advancedMusicPlayer.PlayVlc(fileName);
        }
        else if (audioType.Equals("mp4", StringComparison.OrdinalIgnoreCase))
        {
            _advancedMusicPlayer.PlayMp4(fileName);
        }
    }
}

// Step 4: Implement the Concrete Class Using the Adapter
public class AudioPlayer : IMediaPlayer
{
    private MediaAdapter? _mediaAdapter;

    public void Play(string audioType, string fileName)
    {
        // Inbuilt support to play mp3 music files
        if (audioType.Equals("mp3", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Playing mp3 file. Name: " + fileName);
        }
        // MediaAdapter is providing support to play other file formats
        else if (audioType.Equals("vlc", StringComparison.OrdinalIgnoreCase) || audioType.Equals("mp4", StringComparison.OrdinalIgnoreCase))
        {
            _mediaAdapter = new MediaAdapter();
            _mediaAdapter.Play(audioType, fileName);
        }
        else
        {
            Console.WriteLine("Invalid media. " + audioType + " format not supported");
        }
    }
}
