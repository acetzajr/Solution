namespace Sol;

internal static class FrameRate
{
    public static int TimeToFrame(float time)
    {
        return (int)(time * Constants.FrameRate);
    }

    public static float FrameToTime(int frame)
    {
        return (float)frame / Constants.FrameRate;
    }
}
