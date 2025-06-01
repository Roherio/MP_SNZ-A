using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable_Script
{
    //SCRIPT EXTERN adaptat pel nostre projecte. Canal de Youtube: Game Code Library. Link al vídeo:  https://www.youtube.com/watch?v=MPP9GLp44Pc 

    //script que determina unes funcions pare per tots els objectes que heretin aquesta classe IInteractable_Script. Tots els objectes interactuables (les fonts, les portes...) hauran de tenir-lo definit al principi i controlaran les accions que es fan a l'interactuar amb ells dintre d'Interact().
    void Interact();
    bool CanInteract();
}
