using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Interact_Script : MonoBehaviour, IInteractable_Script
{
    public bool isInteracted { get; private set; } = false;
    public string itemID { get; private set; }
    public GameObject itemPrefab; //el item que nos dará este objeto encuanto interactuemos con el
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
        //meter item en el inventario
        TakeItem();
        Destroy(gameObject);
    }
    public void TakeItem()
    {
        SetTaken(true);
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
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