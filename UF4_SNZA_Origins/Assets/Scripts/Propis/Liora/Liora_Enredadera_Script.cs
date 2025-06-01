using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liora_Enredadera_Script : MonoBehaviour
{
    //public static bool isClimbing;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enredadera"))
        {
            GetComponentInParent<Liora_Movement_Script>()?.CheckEscaleraEnredadera("Enredadera", true);
            //isClimbing = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enredadera"))
        {
            GetComponentInParent<Liora_Movement_Script>()?.CheckEscaleraEnredadera("Enredadera", false);
            //isClimbing = false;
        }
    }
}
