using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Liora_Attack_Script;

public class Liora_StateMachine_Script : MonoBehaviour
{
    //-----------------StateMachine Logic
    State state;
    //states movimiento
    public Liora_Idle_Script idleState;
    public Liora_Run_Script runState;
    public Liora_Airborne_Script airState;
    public Liora_Ledge_Script ledgeState;
    //states ataque
    public Liora_CrabAttack_Script crabAttackState;
    public Liora_CrabParry_Script crabParryState;
    public Liora_BoarAttack_Script boarAttackState;
    public Liora_BoarParry_Script boarParryState;

    private bool isFacingRight = true;
    public Animator animator;
    public Rigidbody2D rb;
    //Ground Logic
    public static bool isGrounded;
    //Jump Logic
    public static float horizontal;
    public bool jumping;
    //Dash Logic
    public static bool isDashing;
    //LedgeGrab Logic
    public static bool isGrabbingLedge;
    //Climb Logic
    public static bool isClimbing;
    //Attack Logic
    public static bool isAttacking;
    public static bool isParrying;
    public static bool isDoingUlti;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //enviament animator pels diferents estats de moviment
        idleState.Setup(rb, animator, horizontal);
        runState.Setup(rb, animator, horizontal);
        airState.Setup(rb, animator, horizontal);
        ledgeState.Setup(rb, animator, horizontal);
        //enviament animator pels diferents estats d'atac
        crabAttackState.Setup(rb, animator, horizontal);
        crabParryState.Setup(rb, animator, horizontal);
        boarAttackState.Setup(rb, animator, horizontal);
        boarParryState.Setup(rb, animator, horizontal);
        state = idleState;
    }
    // Update is called once per frame
    void Update()
    {
        //pas de variables a la state machine
        state.horizontal = horizontal;
        state.isGrounded = isGrounded;
        state.isGrabbingLedge = isGrabbingLedge;
        state.isAttacking = isAttacking;
        state.isParrying = isParrying;
        state.isDoingUlti = isDoingUlti;
        //amb aquest If evitem que el jugador pugui canviar de sprite si esta fent dash
        if (isDashing) { return; }

        if (!isFacingRight && horizontal > 0f)
        {
            FlipSprite();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            FlipSprite();
        }
        if (state.isComplete)
        {
            SelectState();
        }
        state.Do();
    }
    void SelectState()
    {
        if(Time.timeScale == 0f) { return; }
        if (isGrounded && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            if (isAttacking || isParrying)
            {
                if (isAttacking)
                {
                    switch (Liora_Attack_Script.currentAttackType)
                    {
                        case snzaAttackType.CANGREJO:
                            state = crabAttackState;
                            break;
                        case snzaAttackType.JABALI:
                            state = boarAttackState;
                            break;
                    }
                }
                if (isParrying)
                {
                    switch (Liora_Attack_Script.currentParryType)
                    {
                        case snzaParryType.CANGREJO:
                            state = crabParryState;
                            break;
                        case snzaParryType.JABALI:
                            state = boarParryState;
                            break;
                    }
                }
            }
            else
            {
                if (Mathf.Abs(horizontal) < Mathf.Epsilon)
                {
                    state = idleState;
                }
                else
                {
                    state = runState;
                }
            }
        }
        else
        {
            if (isGrabbingLedge == true)
            {
                state = ledgeState;
            }
            else
            {
                state = airState;
            }
        }
        state.Enter();
    }
    private void FlipSprite()
    {
        if (isGrabbingLedge || isClimbing || Time.timeScale == 0f) { return; }
        isFacingRight = !isFacingRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}