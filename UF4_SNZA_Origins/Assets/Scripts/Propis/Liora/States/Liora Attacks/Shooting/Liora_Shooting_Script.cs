using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liora_Shooting_Script : State
{
    public override void Enter()
    {
        animator.speed = 1;
        animator.Play("Throw");
    }
    public override void Do()
    {
        if (!isShooting)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}