using System.Collections.Generic;
using UnityEngine;

public static class InvertionSystem
{
    private static List<InvertableBehaviour> invertables;

    public static bool IsInverted { get; private set; }

    static InvertionSystem()
    {
        invertables = new List<InvertableBehaviour>();
    }

    public static bool Register(InvertableBehaviour invertable)
    {
        if (invertables.Contains(invertable))
            return false;

        invertables.Add(invertable);
        return true;
    }

    public static bool Unregister(InvertableBehaviour invertable)
    {
        if (!invertables.Contains(invertable))
            return false;

        invertables.Remove(invertable);
        return true;
    }

    public static void Invert()
    {
        IsInverted = !IsInverted;

        foreach (InvertableBehaviour invertable in invertables)
            invertable.SetInvertable(IsInverted);
    }

    public static void RandomizeInvertion()
    {
        foreach (InvertableBehaviour invertable in invertables)
            invertable.SetInvertable(Random.Range(0, 2) == 1);
    }
}