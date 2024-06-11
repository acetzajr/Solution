namespace Sol;

public delegate float WaveForm(float x);

public static class WaveForms
{
    public static float Sin(float x)
    {
        return MathF.Sin(2.0f * MathF.PI * x);
    }

    public static float Tri(float x)
    {
        if (x < 0.25f)
        {
            return 4.0f * x;
        }
        if (x < 0.75f)
        {
            return 2.0f - (4.0f * x);
        }
        return (4.0f * x) - 4.0f;
    }

    public static float Sqr(float x)
    {
        return x < 0.5f ? 1.0f : -1.0f;
    }

    public static float Saw(float x)
    {
        return 1.0f - (2.0f * x);
    }
}
