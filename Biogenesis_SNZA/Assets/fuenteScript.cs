using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fuenteScript : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        print("On TriggerBox");
        if (Input.GetKey(KeyCode.E))
        {
            animator.SetBool("firstInteraction", true);
        }

    }
}
