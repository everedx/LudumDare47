using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 Vec2To3(Vector2 v)
    {
        return new UnityEngine.Vector3(v.x, v.y, 0);
    }


    public static Vector2 GetRandomPointOnScreen()
    {
        return new Vector2(
                    Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x),
                     Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y)
                );
    }
}
