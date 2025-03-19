using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Liora_Idle_Script : State
{
    public override void Enter()
    {
        animator.speed = 1;
        animator.Play("Idle");
    }
    public override void Do()
    {
        if (!isGrounded || horizontal != 0f)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}
