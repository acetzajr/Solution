using NAudio.Midi;

namespace Sol;

internal sealed class Midi : IDisposable
{
    public static Midi Instance { get; } = new();

    private Midi()
    {
        midi.MessageReceived += MessageReceived;
        midi.ErrorReceived += ErrorReceived;
    }

    public delegate void MidiEventHandler(MidiEvent @event);
    public event MidiEventHandler? OnMidiEvent;

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

    void ErrorReceived(object? sender, MidiInMessageEventArgs @event)
    {
        throw new Exception(
            $"Time {@event.Timestamp} Message 0x{@event.RawMessage:X8} Event {@event.MidiEvent}"
        );
    }

    void MessageReceived(object? sender, MidiInMessageEventArgs eventArgs)
    {
        var @event = MidiEvent.Parse(eventArgs.RawMessage);
        if (@event is not null)
        {
            OnMidiEvent?.Invoke(@event);
        }
    }

    public void Dispose()
    {
        midi.Dispose();
    }
}
