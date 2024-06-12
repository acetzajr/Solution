using NAudio.Wave;

namespace Sol;

internal class Provider : ISampleProvider
{
    private static readonly WaveFormat waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(
        Constants.FrameRate,
        Constants.Channels
    );
    public WaveFormat WaveFormat => waveFormat;

    public int Read(float[] buffer, int offset, int count)
    {
        if (Audio.Instance.Stopped)
        {
            Console.WriteLine("stopped");
            return 0;
        }
        int read = 0;
        while (count-- > 0)
        {
            buffer[offset + read++] = Audio.Instance.Buffer.NextSample();
            //Console.WriteLine(buffer[offset + read - 1]);
        }
        return read;
    }
}
