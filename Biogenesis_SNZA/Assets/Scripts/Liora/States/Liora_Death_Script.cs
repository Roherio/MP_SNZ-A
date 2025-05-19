using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Liora_Death_Script : State
{
    public override void Enter()
    {
        animator.speed = 1f;
        animator.Play("Death");
    }
    public override void Do()
    {
        if (!isDying)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}
