using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liora_Airborne_Script : State
{
    public override void Enter()
    {
        animator.speed = 1;
        animator.Play("Jump");
    }
    public override void Do()
    {
        float time = Helpers.Map(rb.velocity.y, jumpPower, -jumpPower, 0, 1, true);
        animator.Play("Jump", 0, time);
        //animator.speed = 0f;
        if (isGrounded || isGrabbingLedge == true)
        {
            //////////////////////////////////////////////////////////////jumping = false;
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}
