using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Liora_Interaction_Script : MonoBehaviour
{
    //determina el objecte o NPC interactuable més proper
    private IInteractable_Script interactableInRange = null;
    //icona per mostrar que es pot interactuar amb X
    public GameObject interactionIcon;
    // Start is called before the first frame update
    void Start()
    {
        interactionIcon.SetActive(false);
    }
    private void Update()
    {
        if (GameControl_Script.isPaused) { return; }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
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
}
