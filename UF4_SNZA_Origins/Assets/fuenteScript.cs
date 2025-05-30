using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fuenteScript : MonoBehaviour
{
    DamageLiora_Script damageLioraScript;
    Animator animator;
    private void Start()
    {
        damageLioraScript = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageLiora_Script>();
        animator = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        print("On TriggerBox");
        if (Input.GetKey(KeyCode.E))
        {
            animator.SetBool("firstInteraction", true);
            GameControl_Script.lifeLiora = GameControl_Script.maxLife;
            damageLioraScript.RefillAllPotions();

        }
        
    }
}
