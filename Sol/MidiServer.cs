using NAudio.CoreAudioApi;
using NAudio.Midi;

namespace Sol;

internal sealed class MidiServer : IDisposable
{
    public MidiServer()
    {
        midi.MessageReceived += MessageReceived;
        midi.ErrorReceived += ErrorReceived;
    }

    private readonly MidiIn midi = new(GetDeviceID());

    public void Start()
    {
        midi.Start();
    }

    public void Stop()
    {
        midi.Stop();
    }

    public static int GetDeviceID()
    {
        for (int device = 0; device < MidiIn.NumberOfDevices; device++)
        {
            if (
                string.Equals(
                    Constants.Midi,
                    MidiIn.DeviceInfo(device).ProductName,
                    StringComparison.Ordinal
                )
            )
            {
                return device;
            }
        }
        throw new Exception($"could not find {Constants.Midi} device");
    }

    void ErrorReceived(object? sender, MidiInMessageEventArgs e)
    {
        throw new Exception($"Time {e.Timestamp} Message 0x{e.RawMessage:X8} Event {e.MidiEvent}");
    }

    void MessageReceived(object? sender, MidiInMessageEventArgs e)
    {
        Console.WriteLine($"Time {e.Timestamp} Message 0x{e.RawMessage:X8} Event {e.MidiEvent}");
    }

    public void Dispose()
    {
        midi.Dispose();
    }
}
