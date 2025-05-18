using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Script : MonoBehaviour, IInteractable_Script
{
    public DamageLiora_Script scriptPociones;
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        ParentEnemy[] allEnemies = FindObjectsOfType<ParentEnemy>();
        foreach (ParentEnemy enemy in allEnemies)
        {
            enemy.Respawn();
        }
        scriptPociones.RefillAllPotions();
        SaveController_Script.SaveGame();
    }
}