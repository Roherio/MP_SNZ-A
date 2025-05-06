using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Liora_TakeItem_Script : State
{
    public override void Enter()
    {
        animator.speed = 1.5f;
        animator.Play("TakeItem");
    }
    public override void Do()
    {
        if (!isTakingItem)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}
