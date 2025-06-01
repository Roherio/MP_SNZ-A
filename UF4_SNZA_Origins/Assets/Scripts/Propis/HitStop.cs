using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    bool waiting=false;
    public void hitStop( float duration )
    {
        if (waiting) { return; }
        Time.timeScale = 0.1f;
        StartCoroutine(hitStopTimer(duration));

    }

    IEnumerator hitStopTimer(float duration )
    {
        waiting = true;
        yield return new WaitForSeconds(duration);
        Time.timeScale = 1f;
        waiting = false;
    }
        
}
