using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Script : MonoBehaviour, IInteractable_Script
{
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        SaveController_Script.SaveGame();
    }

}