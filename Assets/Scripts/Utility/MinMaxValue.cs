using System;

[Serializable]
public struct MinMaxValue<T>
{
    public T min;
    public T max;

    public MinMaxValue(T min, T max)
    {
        this.min = min;
        this.max = max;
    }
}