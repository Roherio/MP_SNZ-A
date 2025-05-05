using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Liora_Climb_Script : State
{
    public override void Enter()
    {
        animator.speed = 1f;
        animator.Play("Climb");
    }
    public override void Do()
    {
        if (!isClimbing)
        {
            isComplete = true;
        }
        //mirar si l'animacio es mou o esta estatica a l'escala
        if (Mathf.Abs(rb.velocity.y) > 0.01f)
        {
            animator.speed = 1f;
        }
        else
        {
            animator.speed = 0f;
        }

    }
    public override void Exit()
    {
        animator.speed = 1f;
    }
}
