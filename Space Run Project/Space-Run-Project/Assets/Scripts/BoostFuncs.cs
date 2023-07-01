using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoostFuncs
{
    public static float OneAndHalfBoost(float val)
    {
        return val * 1.5f;
    }

    public static float DoubleBoost(float val)
    {
        return val * 2;
    }

    public static float RandomBoost(float val)
    {
        return val * Random.Range(0.5f, 1.5f);
    }
}
