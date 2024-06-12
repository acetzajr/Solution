namespace Sol;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        using var audio = Audio.Instance;
        using var midi = Midi.Instance;
        using var synth = Synth.Instance;
        using var processor = Processor.Instance;
        midi.OnMidiEvent += synth.OnMidiEvent;
        synth.Start();
        processor.Start();
        audio.Start();
        midi.Start();
        Console.WriteLine("Press enter to exit");
        Console.ReadLine();
        Console.WriteLine("Goodbye...");
        midi.Stop();
        audio.Stop();
        processor.Stop();
        synth.Stop();
    }
}
