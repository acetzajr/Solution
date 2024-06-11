namespace Sol;

public static class Tempered
{
    public static float Frequency(int note, float baseFrequency = 440.0f)
    {
        return baseFrequency * MathF.Pow(2.0f, note / 12.0f);
    }
}
