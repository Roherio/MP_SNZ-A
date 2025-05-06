using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaCabra_Interact_Script : MonoBehaviour, IInteractable_Script
{
    public bool isInteracted { get; private set; } = false;
    public bool CanInteract()
    {
        return GameControl_Script.poderRumo;
    }
    public void Interact()
    {
        if (!CanInteract()) { return; }
        //-----------------------------------------------------poner state breakingWall
        //accionar y abrir la puerta
        Invoke("BreakDoor", 1f); //----------------------------aqui poner el tiempo que tarda la animación de la cabra en dar el golpe
    }
    public void BreakDoor()
    {
        SetUsed(true);
        Destroy(gameObject);
        /*if (door != null)
        {
            Destroy(door);
        }*/
    }
    public void SetUsed(bool used)
    {
        isInteracted = used;
        //si hacemos animacion de accionada
        /*
         if (isInteracted)
         {
              GetComponent<SpriteRenderer>().sprite = openedSprite;
         }
         */
    }
}
