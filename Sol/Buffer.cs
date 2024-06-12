namespace Sol;

internal sealed class Buffer
{
    public Buffer(int frames)
    {
        for (int block = 0; block < Blocks; block++)
        {
            blocks[block] = new Block(frames, Constants.Channels);
        }
    }

    public delegate void BlockNotReadyHandler(Block block);
    public event BlockNotReadyHandler? OnBlockNotReady;
    public static int Blocks => 3;

    private int last;
    private int current;
    private int sampleIndex;
    private readonly Block[] blocks = new Block[Blocks];
    private Block Current => blocks[current];
    private Block Last => blocks[last];

    public float NextSample()
    {
        if (sampleIndex >= Current.Samples)
        {
            if (!Swap())
            {
                Console.WriteLine("Could not swap");
                return 0.0f;
            }
        }
        return Current[sampleIndex++];
    }

    public bool Swap()
    {
        if (!Last.Ready)
        {
            return false;
        }
        Current.Ready = false;
        Current.Clear();
        OnBlockNotReady?.Invoke(Current);
        current = (current + 1) % Blocks;
        last = (last + 1) % Blocks;
        sampleIndex = 0;
        return true;
    }
}
