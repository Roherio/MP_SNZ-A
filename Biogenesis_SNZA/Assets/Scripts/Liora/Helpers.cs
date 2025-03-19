using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    //funció que permet mapejar un valor a dintre d'un rang que decideixes (generalment decidirem entre 0 i 1)
    public static float Map(float value, float min1, float max1, float min2, float max2, bool clamp = false)
    {
        float val = min2 + (max2 - min2) * ((value - min1) / (max1 - min1));
        return clamp ? Mathf.Clamp(val, Mathf.Min(min2, max2), Mathf.Max(min2, max2)) : val;
    }
}
