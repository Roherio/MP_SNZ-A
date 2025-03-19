using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liora_Ledge_Script : State
{
    public override void Enter()
    {
        animator.speed = 1;
        animator.Play("Ledge");
    }
    public override void Do()
    {
        if (!isGrabbingLedge)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}