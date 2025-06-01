using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Amb aquest script podem controlar el efecte HitStop o Hitlag, basicament fa que el joc es pari durant un breu moment de temps, nosaltres
//ho utilizem quan el jugador rep un cop o quan un enemic es mor, pero donar millor feedback al jugador sobre les accions
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
