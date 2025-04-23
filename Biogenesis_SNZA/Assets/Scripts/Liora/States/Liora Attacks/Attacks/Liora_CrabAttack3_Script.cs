using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liora_CrabAttack3_Script : State
{
    public override void Enter()
    {
        animator.speed = 1;
        animator.Play("AttackCrab3");
    }
    public override void Do()
    {
        if (!isAttacking)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}