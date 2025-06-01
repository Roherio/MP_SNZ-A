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
        //aqui utilitzem el script Helpers per mapejar el valor de quin punt de l'animació reproduim. En funció del moment del salt (valor de la velocitat en Y) li tornarem a l'animació de Jump el moment de l'animació que ha de reproduir (amb la variable time)
        float time = Helpers.Map(rb.velocity.y, jumpPower, -jumpPower, 0, 1, true);
        animator.Play("Jump", 0, time);
        //animator.speed = 0f;
        if (isGrounded || isGrabbingLedge == true)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}
