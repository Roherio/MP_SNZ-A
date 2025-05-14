using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaCabra_Interact_Script : MonoBehaviour, IInteractable_Script
{
    public bool isInteracted { get; private set; } = false;
    public bool CanInteract()
    {
        return EventsManager_Script.poderRumo; //---------------------------------------aqui hace falta poner GameControl_Script.poderRumo para que solo se pueda si poderRumo es true
    }
    public void Interact()
    {
        if (!CanInteract()) { return; }
        Liora_StateMachine_Script.isBreakingWall = true;
        Invoke("AnimationBreakDoor", 1.8f); //aqui poner el tiempo que tarda la animación de la cabra en dar el golpe
        Invoke("StopAnimation", 2.2f); //cambio del estado de la animación
        Invoke("BreakDoor", 2.3f); //destruye el gameObject NO LO HAGAS CON UN TIEMPO MENOR QUE EL STOP ANIMATION O DESTRUIR EL GAMEOBJECT NO DEJARA QUE VUELVAS A IDLE
    }
    public void AnimationBreakDoor()
    {
        SetUsed(true); //para hacer desaparecer el icono de Interact
        //----------------------------------------------------poner la animación del muro derrumbandose
    }
    public void BreakDoor()
    {
        SetUsed(true);
        Destroy(gameObject);
    }
    public void StopAnimation()
    {
        Liora_StateMachine_Script.isBreakingWall = false;
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
