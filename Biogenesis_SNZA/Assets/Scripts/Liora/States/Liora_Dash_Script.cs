using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Liora_Dash_Script : State
{
    public override void Enter()
    {
        animator.speed = 1f;
        animator.Play("Dash");
    }
    public override void Do()
    {
        if (!isDashing)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}
