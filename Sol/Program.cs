namespace Sol;

internal static class Program
{
    [STAThread]
    private static void Main() { }

    public static void Sol()
    {
        using var processor = Processor.Instance;
        Buffer.Instance.BlockNotReady += processor.BlockNotReady;
        processor.Start();
        processor.Stop();
    }
}
