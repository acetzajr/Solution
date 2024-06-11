namespace Sol;

public enum Phase
{
    Idle,
    Attack,
    Hold,
    Decay,
    Sustain,
    Release
}

internal class State(int key)
{
    public Phase Phase { get; set; } = Phase.Idle;
    public float Frequency { get; } = Acetza.Frequency(key);
    public float Amplitude { get; set; }
    public int Frame { get; set; }
}
