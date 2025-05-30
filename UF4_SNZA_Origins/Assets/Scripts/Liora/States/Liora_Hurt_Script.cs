using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Liora_Hurt_Script : State
{
    public override void Enter()
    {
        animator.speed = 1;
        animator.Play("Hurt");
    }
    public override void Do()
    {
        if (!isKnockedBack)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {
        Liora_Attack_Script.isAttacking = false;
        Liora_Attack_Script.isParrying = false;
        Liora_Attack_Script.currentComboStep = 0;
        Liora_Attack_Script.canReceiveNextComboInput = true;
    }
}