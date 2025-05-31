using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Interact_Script : MonoBehaviour, IInteractable_Script
{

    public AudioClip takeItemSFX;
    public bool isInteracted { get; private set; } = false;
    public string itemID { get; private set; }

    public string itemUser;
    public string itemName;
    //nom amb el que es mostrara el popup: pot ser Barra de acero, Muelle metalico, Tapones de oidos, Manta protectora, Anillo de Ardilla, Anillo de Cabra, Cristalizador, Cuero, Vial
    public string itemUIName;

    public Sprite spriteBarraKhione;
    public Sprite spriteMuelleKhione;
    public Sprite spriteTaponesRumo;
    public Sprite spriteMantaRumo;
    public Sprite spriteCristalizador;
    public Sprite spriteCuero;
    public Sprite spriteVial;
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
        if (itemName == "cristalizador")
        {
            //esto se retirará porque los cristalizadores te los dan o Gander o Wallace
            ItemPopUp_Script.Instance.ShowItemPickup(itemUIName, spriteCristalizador);
        }
        if (itemName == "cuero")
        {
            ItemPopUp_Script.Instance.ShowItemPickup(itemUIName, spriteCuero);
        }
        if (itemName == "vial")
        {
            ItemPopUp_Script.Instance.ShowItemPickup(itemUIName, spriteVial);
        }
    }
    public void StopAnimation()
    {
        Liora_StateMachine_Script.isTakingItem = false;
    }
    public void DestroyObject()
    {
        AudioSource.PlayClipAtPoint(takeItemSFX,transform.position);
        Destroy(gameObject);
    }
    public void SetTaken(bool taken)
    {
        isInteracted = taken;
    }
}