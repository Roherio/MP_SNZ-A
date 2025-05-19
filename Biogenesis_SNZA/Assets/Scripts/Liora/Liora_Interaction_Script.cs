using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Liora_Interaction_Script : MonoBehaviour
{
    //determina el objecte o NPC interactuable més proper
    private IInteractable_Script interactableInRange = null;
    //icona per mostrar que es pot interactuar amb X
    public GameObject interactionIcon;

    //private bool isFacingRight = true;
    
    // Start is called before the first frame update
    void Start()
    {
        interactionIcon.SetActive(false);
    }
    private void Update()
    {
        /*if (!isFacingRight && Liora_StateMachine_Script.horizontal > 0f)
        {
            FlipSprite();
        }
        else if (isFacingRight && Liora_StateMachine_Script.horizontal < 0f)
        {
            FlipSprite();
        }*/
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (GameControl_Script.isPaused) { return; }
        if (context.performed)
        {
            interactableInRange?.Interact();
        }
    }
    
    //funcions per determinar si mostrar o no la icona de Interact
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //mirarem si el objecte amb el que colisiona té associat el script interactable && es pot interactuar amb ell
        if (collision.TryGetComponent(out IInteractable_Script interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //mirarem si el objecte amb el que colisiona té associat el script interactable && era el mateix que hem guardat en interactableInRange (el més proper)
        if (collision.TryGetComponent(out IInteractable_Script interactable) && interactable == interactableInRange)
        {
            //no guardem cap interactable a la variable i desactivem la icona que marca interacció
            interactableInRange = null;
            interactionIcon.SetActive(false);
        }
    }
    /*private void FlipSprite()
    {
        if (Liora_StateMachine_Script.isTakingItem || Liora_StateMachine_Script.isBreakingWall || Liora_StateMachine_Script.isGrabbingLedge || Liora_StateMachine_Script.isClimbing || Time.timeScale == 0f || Liora_Attack_Script.isAttacking || Liora_Attack_Script.isParrying || Liora_Attack_Script.isDoingUlti) { return; }
        isFacingRight = !isFacingRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }*/
}
