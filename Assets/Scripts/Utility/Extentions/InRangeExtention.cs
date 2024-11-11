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

    public static bool InRange(this float value, MinMaxValue<float> range)
    {
        return value >= range.min && value <= range.max;
    }

    public static bool InRange(this int value, MinMaxValue<int> range)
    {
        return value >= range.min && value <= range.max;
    }
}