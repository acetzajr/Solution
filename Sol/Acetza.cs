namespace Sol;

internal static class Acetza
{
    private static float[] rations =
    [
        (float)1 / 1,
        (float)256 / 243,
        (float)9 / 8,
        (float)32 / 27,
        (float)81 / 64,
        (float)4 / 3,
        Tempered.Frequency(6, baseFrequency: 1.0f),
        (float)3 / 2,
        (float)128 / 81,
        (float)27 / 16,
        (float)16 / 9,
        (float)243 / 128
    ];
    public static float baseFrequency = Constants.Base;

    public static float Frequency(int key)
    {
        return rations[key % 12] * baseFrequency * MathF.Pow(2.0f, key / 12);
    }
}
