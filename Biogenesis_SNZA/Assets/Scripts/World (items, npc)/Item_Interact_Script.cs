using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Interact_Script : MonoBehaviour, IInteractable_Script
{
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
        //----------------logica per destruir els game objects en el moment de carregar partida SI JA ES DETECTA QUE S'HAN RECOLECTAT EN ARXIUS GUARDATS
        if (itemUser == "Khione")
        {
            if (itemName == "barraKhione")
            {
                if (EventsManager_Script.barraObtenida)
                {
                    Destroy(gameObject);
                }
            }
            else if (itemName == "muelleKhione")
            {
                if (EventsManager_Script.muelleObtenido)
                {
                    Destroy(gameObject);
                }
            }
        }
        if (itemUser == "Rumo")
        {
            if (itemName == "taponesRumo")
            {
                if (EventsManager_Script.taponesObtenidos)
                {
                    Destroy(gameObject);
                }
            }
            else if (itemName == "mantaRumo")
            {
                if (EventsManager_Script.mantaObtenida)
                {
                    Destroy(gameObject);
                }
            }
        }
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
        if (itemUser == "Khione")
        {
            if (itemName == "barraKhione")
            {
                ItemPopUp_Script.Instance.ShowItemPickup(itemUIName, spriteBarraKhione);
                EventsManager_Script.ActivarObjKhione1();
            }
            else if (itemName == "muelleKhione")
            {
                ItemPopUp_Script.Instance.ShowItemPickup(itemUIName, spriteMuelleKhione);
                EventsManager_Script.ActivarObjKhione2();
            }
        }
        if (itemUser == "Rumo")
        {
            if (itemName == "taponesRumo")
            {
                ItemPopUp_Script.Instance.ShowItemPickup(itemUIName, spriteTaponesRumo);
                EventsManager_Script.ActivarObjRumo1();
            }
            else if (itemName == "mantaRumo")
            {
                ItemPopUp_Script.Instance.ShowItemPickup(itemUIName, spriteMantaRumo);
                EventsManager_Script.ActivarObjRumo2();
            }
            
        }
        if (itemName == "cristalizador")
        {
            //esto se retirará porque los cristalizadores te los dan o Gander o Wallace
            GameControl_Script.cristalizadores++;
            ItemPopUp_Script.Instance.ShowItemPickup(itemUIName, spriteCristalizador);
            BotonesActivos_Script.bCristalizador = true;
            SNZAProgressJabali_Script.bTachadoCristalizadorJabali = true;
            SNZAProgressSecretario_Script.bTachadoCristalizadorSecretario = true;
        }
        if (itemName == "cuero")
        {
            ItemPopUp_Script.Instance.ShowItemPickup(itemUIName, spriteCuero);
            GameControl_Script.cuero++;
            BotonesActivos_Script.bCuero = true;
        }
        if (itemName == "vial")
        {
            ItemPopUp_Script.Instance.ShowItemPickup(itemUIName, spriteVial);
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