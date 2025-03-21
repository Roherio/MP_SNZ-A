using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Liora_StateMachine_Script : MonoBehaviour
{
    //StateMachine Logic
    public Liora_Idle_Script idleState;
    public Liora_Run_Script runState;
    public Liora_Airborne_Script airState;
    public Liora_Ledge_Script ledgeState;
    State state;

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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        idleState.Setup(rb, animator, horizontal);
        runState.Setup(rb, animator, horizontal);
        airState.Setup(rb, animator, horizontal);
        ledgeState.Setup(rb, animator, horizontal);
        state = idleState;
    }
    // Update is called once per frame
    void Update()
    {
        //pas de variables a la state machine
        state.horizontal = horizontal;
        state.isGrounded = isGrounded;
        state.isGrabbingLedge = isGrabbingLedge;
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
        if (isGrounded && Mathf.Abs(rb.velocity.y) < Mathf.Epsilon)
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
        isFacingRight = !isFacingRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}