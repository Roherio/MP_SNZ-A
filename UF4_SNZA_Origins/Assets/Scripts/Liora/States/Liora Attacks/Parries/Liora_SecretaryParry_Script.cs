using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liora_SecretaryParry_Script : State
{
    public override void Enter()
    {
        animator.speed = 1;
        animator.Play("ParrySecretary");
    }
    public override void Do()
    {
        if (!isParrying)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}