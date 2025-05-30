using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Script : MonoBehaviour, IInteractable_Script
{
    DamageLiora_Script damageLioraScript;
    public Animator animator;
    private void Start()
    {
        damageLioraScript = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageLiora_Script>();
        animator = GetComponent<Animator>();
    }
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        animator.SetBool("firstInteraction", true);
        ParentEnemy[] allEnemies = FindObjectsOfType<ParentEnemy>();
        foreach (ParentEnemy enemy in allEnemies)
        {
            enemy.Respawn();
        }
        GameControl_Script.lifeLiora = GameControl_Script.maxLife;
        damageLioraScript.RefillAllPotions();
        SaveController_Script.SaveGame();
    }
}