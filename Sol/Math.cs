namespace Sol;

public static class Math
{
    public static int PMod(int n, int mod)
    {
        return n < 0 ? ((n % mod) + mod) % mod : n % mod;
    }

    public static float ToDB(float amplitude)
    {
        return 10.0f * MathF.Log10(amplitude);
    }

    public static float FromDB(float db)
    {
        return MathF.Pow(10.0f, db / 10.0f);
    }
}
