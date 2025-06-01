using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    //funci� que permet mapejar un valor a dintre d'un rang que decideixes (generalment decidirem entre 0 i 1) aix� ens permetr� que l'animaci� del salt sempre es mostri en funci� de si el personatge est� pujant o baixant, per exemple, i no que depengui nom�s del temps. Si no fos aix�, en un salt llarg cap a un terra molt lluny� cap avall veuriem l'animaci� de salt en loop. d'aquesta manera, veiem que es mant� l'�ltim frame fins que toca el terra. tamb�, com que el personatge pot saltar m�s o menys depenent de si mantenim presionat el bot�, si fos un salt curt no correspondrien alguns dels frames de l'animaci� total.
    public static float Map(float value, float min1, float max1, float min2, float max2, bool clamp = false)
    {
        float val = min2 + (max2 - min2) * ((value - min1) / (max1 - min1));
        return clamp ? Mathf.Clamp(val, Mathf.Min(min2, max2), Mathf.Max(min2, max2)) : val;
    }
}
