using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //script base per tots els GameObject interactuables (NPCs, Personatges secundaris, objectes recol·lectables i mecanismes del món
    public bool canInteract;
    public virtual void Interact() { }
}
