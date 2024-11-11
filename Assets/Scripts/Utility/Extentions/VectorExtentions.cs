using UnityEngine;

public static class VectorExtentions
{
    // Vector.Distance
    public static float Distance(this Transform from, Transform to)
    {
        return Vector3.Distance(from.position, to.position);
    }

    public static float Distance(this Vector3 from, Vector3 to)
    {
        return Vector3.Distance(from, to);
    }

    // Vector.Angle
    public static float Angle(this Vector3 from, Vector3 to)
    {
        return Vector3.Angle(from, to);
    }

    public static float Angle(this Transform from, Transform to)
    {
        return Vector3.Angle(from.eulerAngles, to.eulerAngles);
    }

    // Direction
    public static Vector3 Direction(this Transform from, Transform to)
    {
        return from.position - to.position;
    }

    public static Vector3 DirectionXZ(this Transform from, Transform to)
    {
        Vector3 direction = from.position - to.position;
        direction.y = 0f;

        return direction;
    }
}