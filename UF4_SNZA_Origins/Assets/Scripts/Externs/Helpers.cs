using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    /*SCRIPT EXTERN modificat per adaptar-se al nostre projecte (Canal de Youtube: AdamCYounis. Link al vídeo: https://www.youtube.com/watch?v=-jkT4oFi1vk (minut 23:20)*/


    //funció que permet mapejar un valor a dintre d'un rang que decideixes (generalment decidirem entre 0 i 1) això ens permetrà que l'animació del salt sempre es mostri en funció de si el personatge està pujant o baixant, per exemple, i no que depengui només del temps. Si no fos així, en un salt llarg cap a un terra molt llunyà cap avall veuriem l'animació de salt en loop. d'aquesta manera, veiem que es manté l'últim frame fins que toca el terra. també, com que el personatge pot saltar més o menys depenent de si mantenim presionat el botó, si fos un salt curt no correspondrien alguns dels frames de l'animació total.
    public static float Map(float value, float min1, float max1, float min2, float max2, bool clamp = false)
    {
        float val = min2 + (max2 - min2) * ((value - min1) / (max1 - min1));
        return clamp ? Mathf.Clamp(val, Mathf.Min(min2, max2), Mathf.Max(min2, max2)) : val;
    }
}
