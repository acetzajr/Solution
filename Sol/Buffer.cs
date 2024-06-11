namespace Sol;

internal sealed class Buffer
{
    public static Buffer Instance { get; } = new();

    private Buffer()
    {
        for (int block = 0; block < Blocks; block++)
        {
            blocks[block] = new Block(Constants.FrameRate, Constants.Channels);
        }
    }

    public delegate void BlockNotReadyHandler(Block block);
    public event BlockNotReadyHandler? BlockNotReady;
    public static int Blocks => 3;

    private int current;
    private int sampleIndex;
    private readonly Block[] blocks = new Block[Blocks];
    private Block Current => blocks[current];

    public float NextSample()
    {
        if (sampleIndex >= Current.Samples)
        {
            Swap();
        }
        return Current[sampleIndex++];
    }

    public void Swap()
    {
        Current.Ready = false;
        BlockNotReady?.Invoke(Current);
        current = (current + 1) % Blocks;
        if (!Current.Ready)
        {
            throw new Exception("current buffer block is not ready");
        }
        sampleIndex = 0;
    }
}
