using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Liora_TakePotion_Script : State
{
    public override void Enter()
    {
        animator.speed = 1f;
        animator.Play("TakePotion");
    }
    public override void Do()
    {
        if (!isTakingPotion)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}
