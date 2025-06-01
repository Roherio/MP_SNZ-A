using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liora_Escalera_Script : MonoBehaviour
{
    //public static bool isClimbing;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escalera"))
        {
            GetComponentInParent<Liora_Movement_Script>()?.CheckEscaleraEnredadera("Escalera", true);
            //isClimbing = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Escalera"))
        {
            GetComponentInParent<Liora_Movement_Script>()?.CheckEscaleraEnredadera("Escalera", false);
            //isClimbing = false;
        }
    }
}
