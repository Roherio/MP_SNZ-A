using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liora_SecretaryAttack2_Script : State
{
    public override void Enter()
    {
        animator.speed = 1;
        animator.Play("AttackSecretary2");
    }
    public override void Do()
    {
        /*if (Liora_Attack_Script.haRecibidoInput = true)
        {
            isComplete = true;
            ataque2
        }*/
        if (!isAttacking)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}