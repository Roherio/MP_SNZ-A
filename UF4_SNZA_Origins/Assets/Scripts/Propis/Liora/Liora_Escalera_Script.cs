using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liora_Escalera_Script : MonoBehaviour
{
    //aquest script est� associat al collider que detecta si hem fet col�lisi� amb un objecte amb tag "escalera". Si �s aix�, aix� ens permetr� canviar a True la variable isClimbing a l'script Liora_Movement_Script (l�nia 254)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escalera"))
        {
            //accedim a l'script movement del pare de l'objecte collider i executem aquesta funci� per passar a l'estat climbing
            GetComponentInParent<Liora_Movement_Script>()?.CheckEscaleraEnredadera("Escalera", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Escalera"))
        {
            GetComponentInParent<Liora_Movement_Script>()?.CheckEscaleraEnredadera("Escalera", false);
        }
    }
}