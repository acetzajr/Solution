using System.Collections.Concurrent;
using NAudio.Wave;

namespace Sol;

internal class Synth : IDisposable
{
    public static Synth Instance { get; } = new();

    public Synth()
    {
        for (int key = 0; key < states.Length; key++)
        {
            states[key] = new(key);
        }
        thread = new Thread(Main);
    }

    private readonly Thread thread;
    private readonly BlockingCollection<object?> messages = [];
    private readonly BlockingCollection<object?> blocking = [];
    private readonly State[] states = new State[Constants.Keys];
    private readonly WaveForm waveform = WaveForms.Sin;

    public void Start()
    {
        thread.Start();
    }

    public void Stop()
    {
        messages.Add(null);
        thread.Join();
    }

    private void Main()
    {
        bool running = true;
        while (running)
        {
            switch (messages.Take())
            {
                case Block block:
                    Process(block);
                    blocking.Add(null);
                    break;
                case null:
                    running = false;
                    break;
                default:
                    throw new Exception("unknown message received");
            }
        }
    }

    private void Process(Block block)
    {
        foreach (var state in states)
        {
            if (state.Phase is Phase.Idle)
            {
                continue;
            }
            ProcessNote(block, state);
        }
    }

    private void ProcessNote(Block block, State state)
    {
        for (int frame = 0; frame < block.Frames; frame++)
        {
            var time = FrameRate.FrameToTime(state.Frame++);
            var part = (time * state.Frequency) % 1.0f;
            var sample = waveform(part) * state.Amplitude;
            for (int channel = 0; channel < block.Channels; channel++)
            {
                block.Add(frame, channel, sample);
            }
        }
    }

    public void BeginBlockProcessing(Block block)
    {
        messages.Add(block);
    }

    public void EndBlockProcessing()
    {
        blocking.Take();
    }

    public void Dispose()
    {
        blocking.Dispose();
        messages.Dispose();
    }
}
