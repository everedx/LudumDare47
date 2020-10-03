using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 Vec2To3(Vector2 v)
    {
        return new UnityEngine.Vector3(v.x, v.y, 0);
    }

}
