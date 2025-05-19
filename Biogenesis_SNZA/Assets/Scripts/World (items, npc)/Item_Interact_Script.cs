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
            if (itemName == "barraKhione")
            {
                EventsManager_Script.ActivarObjKhione1();
            }
            else if (itemName == "muelleKhione")
            {
                EventsManager_Script.ActivarObjKhione2();
            }
        }
        if (itemUser == "Rumo")
        {
            if (itemName == "taponesRumo")
            {
                EventsManager_Script.ActivarObjRumo1();
            }
            else if (itemName == "mantaRumo")
            {
                EventsManager_Script.ActivarObjRumo2();
            }
            
        }
        if (itemName == "Cristalizador")
        {
            //esto se retirará porque los cristalizadores te los dan o Gander o Wallace
            GameControl_Script.cristalizadores++;
            BotonesActivos_Script.bCristalizador = true;
            SNZAProgress_Script.bTachadoCristalizadorJabali = true;
            SNZAProgress_Script.bTachadoCristalizadorSecretario = true;
        }
        if (itemName == "Cuero")
        {
            GameControl_Script.cuero++;
            BotonesActivos_Script.bCuero = true;
        }
        if (itemName == "Vial")
        {
            GameControl_Script.viales++;
            BotonesActivos_Script.bVial = true;
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