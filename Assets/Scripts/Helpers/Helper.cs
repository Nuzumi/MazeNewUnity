using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper  {

    public static float distance(Vector2 vector)
    {
        return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
    }

    public static float distance(Vector2 start, Vector2 end)
    {
        return Mathf.Sqrt(Mathf.Pow(end.x - start.x, 2) + Mathf.Pow(end.y - start.y, 2));
    }

    public static float distance(Vector3 start, Vector3 end)
    {
        return Mathf.Sqrt(Mathf.Pow(start.x - end.x, 2) + Mathf.Pow(start.y - end.y, 2) + Mathf.Pow(start.z - end.z,2));

    }

    public static Vector2 getVector(Vector2 start, Vector2 end)
    {
        return new Vector2(end.x - start.x, end.y - start.y);
    }

    public static Vector2 getVersor(Vector2 start, Vector2 end)
    {
        Vector2 vector = getVector(start, end);
        float length = distance(start, end);
        return vector / length;
    }

    public static Vector2 getVersor(Vector2 vector)
    {
        return vector / distance(vector);
    }

}
