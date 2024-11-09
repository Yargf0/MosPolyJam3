using UnityEngine;

public static class DistanceExtention
{
    public static float Distance(this Transform from, Transform to)
    {
        return Vector3.Distance(from.position, to.position);
    }

    public static float Distance(this Vector3 from, Vector3 to)
    {
        return Vector3.Distance(from, to);
    }
}