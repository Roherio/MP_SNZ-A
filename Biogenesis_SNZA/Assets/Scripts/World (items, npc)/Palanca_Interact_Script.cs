using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca_Interact_Script : MonoBehaviour, IInteractable_Script
{
    public AudioClip clip;
    public bool isInteracted { get; private set; } = false;
    public GameObject door; //la puerta que se abrirá cuando interactues con la palanca. Asignada en el inspector.
    public bool CanInteract()
    {
        return !isInteracted;
    }
    public void Interact()
    {
        if (!CanInteract()) { return; }
        //accionar y abrir la puerta
        OpenDoor();
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
    public void OpenDoor()
    {
        SetUsed(true);
        if (door != null)
        {
            Destroy(door);
        }
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