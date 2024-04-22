using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ass1Intagrator
{
    public static Vector2 Integrate(Vector2 vel, Vector2 pos, float dt)
    {

        return pos += vel * dt;

    }
    /*
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    */
}
