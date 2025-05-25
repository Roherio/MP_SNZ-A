using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractIcon_Script : MonoBehaviour
{
    //script unicament per fer flip a la icona de la E de interact
    void Update()
    {
        if (!Liora_StateMachine_Script.isFacingRight)
        {
            Vector2 scale = transform.localScale;
            scale.x = -0.75f;
            transform.localScale = scale;
        }
        else
        {
            Vector2 scale = transform.localScale;
            scale.x = 0.75f;
            transform.localScale = scale;
        }
    }
}
