using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Liora_BreakWall_Script : State
{
    public override void Enter()
    {
        animator.speed = 1.5f;
        animator.Play("BreakWall");
    }
    public override void Do()
    {
        if (!isBreakingWall)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}
