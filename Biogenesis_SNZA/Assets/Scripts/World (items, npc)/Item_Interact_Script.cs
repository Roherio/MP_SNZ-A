using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Interact_Script : MonoBehaviour, IInteractable_Script
{
    public bool isInteracted { get; private set; } = false;
    public string itemID { get; private set; }

    public string itemUser;
    public string itemName;
    //public Sprite usedSprite; en el caso de que tuvieramos objetos tipo cofre que se quedan abiertos
    private void Start()
    {
        itemID ??= HelperItems_Script.GenerateUniqueID(gameObject);
    }
    public bool CanInteract()
    {
        return !isInteracted;
    }
    public void Interact()
    {
        if (!CanInteract()) { return; }
        //------------------------------------meter item en el inventario. en el caso de que aun no tenemos el inventario acabado, estamos simplemente actualizando la variable en caso de ser objeto de Khione o Rumo.
        Liora_StateMachine_Script.isTakingItem = true;
        Invoke("TakeItem", 1f);//----------------poner el tiempo que tarda la animacion en agacharse y coger el objeto
        Invoke("StopAnimation", 2.1f);
        Invoke("DestroyObject", 2.2f);
    }
    public void TakeItem()
    {
        SetTaken(true);
        print("takenItem!!!!!!!!!!");
        if (itemUser == "Khione")
        {
            EventsManager_Script.piezasKhione++;
        }
        if (itemUser == "Rumo")
        {
            if (itemName == "cascosRumo")
            {
                EventsManager_Script.cascosRumo = true;
            }
            else if (itemName == "mantaRumo")
            {
                EventsManager_Script.mantaRumo = true;
            }
            
        }
    }
    public void StopAnimation()
    {
        Liora_StateMachine_Script.isTakingItem = false;
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    public void SetTaken(bool taken)
    {
        isInteracted = taken;
        //si hacemos animacion de cogido:
        /*
         if (isInteracted)
         {
              GetComponent<SpriteRenderer>().sprite = openedSprite;
         }
         */
    }
}