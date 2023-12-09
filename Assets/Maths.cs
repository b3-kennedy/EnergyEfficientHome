using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Maths
{
    public static float RoundTo2DP(float value)
    {
        return Mathf.Round(value * 10) * 0.1f;
    }
}
