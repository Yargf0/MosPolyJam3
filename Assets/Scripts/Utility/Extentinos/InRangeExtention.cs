public static class InRangeExtention
{
    public static bool InRange(this float value, float min, float max)
    {
        return value >= min && value <= max;
    }

    public static bool InRange(this int value, int min, int max)
    {
        return value >= min && value <= max;
    }
}