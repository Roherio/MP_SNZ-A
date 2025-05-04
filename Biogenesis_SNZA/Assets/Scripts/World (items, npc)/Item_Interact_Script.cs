using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Interact_Script : MonoBehaviour, IInteractable_Script
{
    public bool isInteracted { get; private set; } = false;
    public string itemID { get; private set; }

    public string itemUser;
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
        //meter item en el inventario. en el caso de que aun no tenemos el inventario acabado, estamos simplemente instanciando un objeto.
        TakeItem();
        Destroy(gameObject);
    }
    public void TakeItem()
    {
        SetTaken(true);
        if (itemUser == "Khione")
        {
            GameControl_Script.piezasKhione++;
            print(GameControl_Script.piezasKhione);
        }
        if (itemUser == "Rumo")
        {
            GameControl_Script.piezasRumo++;
        }
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