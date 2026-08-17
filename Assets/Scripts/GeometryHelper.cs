using UnityEngine;

public static class GeometryHelper
{
    [Tooltip("Returns a new Vector2 that is the result of rotating the given direction vector by the specified angle in degrees.")]
    public static Vector2 plusAdirectionByAngle(Vector2 direction, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(direction.x * cos - direction.y * sin, direction.x * sin + direction.y * cos);
    }   
}
