using NAudio.Wave;

namespace Sol;

internal sealed class Audio : IDisposable
{
    public static Audio Instance { get; } = new();

    private Audio()
    {
        asio.Init(provider);
    }

    private readonly AsioOut asio = new("FL Studio ASIO");
    private readonly Provider provider = new();

    public void Start()
    {
        Console.WriteLine(asio.PlaybackLatency);
        Console.WriteLine(asio.FramesPerBuffer);
    }

    public void Dispose()
    {
        asio.Dispose();
    }
}
