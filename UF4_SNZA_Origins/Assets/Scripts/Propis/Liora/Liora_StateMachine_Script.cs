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
    public Liora_Climb_Script climbState;
    public Liora_Dash_Script dashState;
    public Liora_TakePotion_Script takePotionState;
    public Liora_Hurt_Script hurtState;
    public Liora_Death_Script deathState;
    //------------------states ataque
    public Liora_CrabAttack1_Script crabAttackState1;
    public Liora_CrabAttack2_Script crabAttackState2;
    public Liora_CrabAttack3_Script crabAttackState3;
    public Liora_Shooting_Script shootingState;
    
    
    //---------------------booleanes per controlar l'estat del jugador
    public static bool isFacingRight = true;
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
    //ACTIONS Logic
    public static bool isTakingPotion;
    public static bool isKnockedBack;
    public static bool isDying;
    //Attack Logic
    public static bool isAttacking;
    public static bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //enviament animator pels diferents estats de moviment
        idleState.Setup(rb, animator, horizontal);
        runState.Setup(rb, animator, horizontal);
        airState.Setup(rb, animator, horizontal);
        ledgeState.Setup(rb, animator, horizontal);
        climbState.Setup(rb, animator, horizontal);
        dashState.Setup(rb, animator, horizontal);
        //enviament animator pels diferents estats d'acció
        takePotionState.Setup(rb, animator, horizontal);
        hurtState.Setup(rb, animator, horizontal);
        deathState.Setup(rb, animator, horizontal);
        //enviament animator pels diferents estats d'atac
        crabAttackState1.Setup(rb, animator, horizontal);
        crabAttackState2.Setup(rb, animator, horizontal);
        crabAttackState3.Setup(rb, animator, horizontal);
        shootingState.Setup(rb, animator, horizontal);
        state = idleState;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameControl_Script.isPaused || GameControl_Script.isPausedDialogue) { return; }
        //pas de variables a la state machine
        state.horizontal = horizontal;
        state.isGrounded = isGrounded;
        state.isGrabbingLedge = isGrabbingLedge;
        state.isClimbing = isClimbing;
        state.isDashing = isDashing;
        //
        state.isTakingPotion = isTakingPotion;
        state.isKnockedBack = isKnockedBack;
        state.isDying = isDying;
        //
        state.isAttacking = isAttacking;
        state.isShooting = isShooting;
        //amb aquests if controlem que el jugador miri cap a la direcció correcta amb el seu sprite
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
        if (Time.timeScale == 0f) { return; }
        if (isDying)
        {
            state = deathState;
        }
        else
        {
            if (isKnockedBack)
            {
                state = hurtState;
            }
            else
            {
                if (isGrounded && Mathf.Abs(rb.velocity.y) < 0.01f)
                {
                    if (isDashing)
                    {
                        state = dashState;
                    }
                    else
                    {
                        if (isClimbing)
                        {
                            state = climbState;
                        }
                        else
                        {
                            if (isTakingPotion)
                            {
                                state = takePotionState;
                            }
                            else
                            {
                                if (isAttacking || isShooting)
                                {
                                    if (isAttacking)
                                    {
                                        switch (currentComboStep)
                                        {
                                            case 1:
                                                state = crabAttackState1;
                                                break;
                                            case 2:
                                                state = crabAttackState2;
                                                break;
                                            case 3:
                                                state = crabAttackState3;
                                                break;
                                        }
                                    }
                                    if (isShooting)
                                    {
                                        state = shootingState;
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
                        }
                    }
                }
                else
                {
                    if (isClimbing)
                    {
                        state = climbState;
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
                }
            }
        }
        state.Enter();
    }
    private void FlipSprite()
    {
        if (isTakingPotion || isGrabbingLedge || isClimbing || Time.timeScale == 0f || Liora_Attack_Script.isAttacking || Liora_Attack_Script.isShooting) { return; }
        isFacingRight = !isFacingRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}