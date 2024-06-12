using System.Collections.Concurrent;

namespace Sol;

internal sealed class Processor : IDisposable
{
    public static Processor Instance { get; } = new();

    private Processor()
    {
        processingThread = new Thread(BlockProcessing);
    }

    private readonly Thread processingThread;
    private readonly BlockingCollection<Block?> blocks = [];

    public void Start()
    {
        processingThread.Start();
    }

    public void Stop()
    {
        blocks.Add(null);
        processingThread.Join();
    }

    public void OnBlockNotReady(Block block)
    {
        blocks.Add(block);
    }

    private void BlockProcessing()
    {
        while (true)
        {
            var block = blocks.Take();
            if (block is null)
            {
                break;
            }
            Synth.Instance.BeginBlockProcessing(block);
            Synth.Instance.EndBlockProcessing();
            block.Ready = true;
        }
    }

    public void Dispose()
    {
        blocks.Dispose();
    }
}
