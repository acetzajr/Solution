namespace Sol;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        using var midi = new MidiServer();
        midi.Start();
        Console.WriteLine("Press enter to exit");
        Console.ReadLine();
        midi.Stop();
    }

    public static void Sol()
    {
        using var processor = Processor.Instance;
        Buffer.Instance.BlockNotReady += processor.BlockNotReady;
        processor.Start();
        processor.Stop();
    }
}
