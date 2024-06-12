namespace Sol;

internal class Block(int frames, int channels)
{
    private readonly object samplesLock = new();
    private readonly float[] samples = new float[frames * channels];
    public int Frames { get; } = frames;
    public int Channels { get; } = channels;
    public int Samples => samples.Length;
    public bool Ready { get; set; } = true;
    public float this[int index] => samples[index];

    public void Clear()
    {
        for (int index = 0; index < Samples; index++)
        {
            samples[index] = 0.0f;
        }
    }

    public void Add(int frame, int channel, float sample)
    {
        lock (samplesLock)
        {
            samples[(frame * Channels) + channel] += sample;
        }
    }
}
