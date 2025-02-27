using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_Liora_Movement : MonoBehaviour
{
    Rigidbody2D rb;
    public BoxCollider2D groundCheck;
    public LayerMask groundLayer;

    enum PlayerState { Idle, Running, Airborne}
    PlayerState playerState;
    bool stateComplete;
    Animator animator;
    
    private float horizontal;
    [SerializeField] float groundSpeed = 5f;
    [SerializeField] float jumpPower = 10f;
    private bool isFacingRight = true;

    public bool jumping;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * groundSpeed, rb.velocity.y);
        if (!isFacingRight && horizontal > 0f)
        {
            FlipSprite();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            FlipSprite();
        }
        if (stateComplete)
        {
            SelectState();
        }
        UpdateState();
        //StateMachine();
    }

    public void Movimiento(InputAction.CallbackContext context)
    {
        //print("moviendosee");
        horizontal = context.ReadValue<Vector2>().x;
    }
    public void Saltar(InputAction.CallbackContext context)
    {
        if (context.started && CheckGround())
        {
            //saltarà amb la primera input (context.started, sinó seria input continu) nomes si isGrounded es vertader
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            //animació jump
            jumping = true;
        }
        if (context.canceled && rb.velocity.y > 0)
        {
            //si deixes de premer el botó (context.canceled) i està pujant encara (rb.velocity.y > 0), tallarà el salt fent que baixi la seva velocitat vertical a la meitat
            rb.AddForce(new Vector2(0, -4f), ForceMode2D.Impulse);
        }
    }
    /*public void StateMachine()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", false);
        if (CheckGround() && rb.velocity.x != 0)
        {
            animator.SetBool("isRunning", true);
        }
        if (CheckGround() && jumping)
        {
            animator.SetBool("isJumping", true);
        }
        if (!CheckGround() && rb.velocity.y < 0)
        {
            animator.SetBool("isFalling", true);
        }
    }*/
    void SelectState()
    {
        stateComplete = false;
        if (CheckGround())
        {
            if (horizontal == 0f)
            {
                playerState = PlayerState.Idle;
                StartIdle();
            }
            else
            {
                playerState = PlayerState.Running;
                StartRunning();
            }
        }
        else
        {
            playerState = PlayerState.Airborne;
            StartAirborne();
        }
    }
    //update functions: comproben les condicions en les que el jugador deixarà
    void UpdateState()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Running:
                UpdateRunning();
                break;
            case PlayerState.Airborne:
                UpdateAirborne();
                break;
        }
    }
    void UpdateIdle()
    {
        if (!CheckGround() || horizontal != 0f)
        {
            stateComplete = true;
        }
    }
    void UpdateRunning()
    {
        if (!CheckGround() || horizontal == 0f)
        {
            stateComplete = true;
        }
    }
    void UpdateAirborne()
    {
        //utilitzem la funció per mapejar el valor del jumpPower del 12 al -12 (de -jumpPower) entre 0 i 1 i decidir quins frames es reprodueixen de l'animacio de salt, per controlar que quan puja es reprodueixi la primera meitat de l'animació i quan baixa la segona
        float time = Map(rb.velocity.y, jumpPower, -jumpPower, 0, 1, true);
        animator.Play("Jump", 0, time);
        //animator.speed = 0f;
        if (CheckGround())
        {
            stateComplete = true;
        }
    }
    void StartIdle()
    {
        animator.speed = 1;
        animator.Play("Idle");
    }
    void StartRunning()
    {
        animator.Play("Run");
        animator.speed = 1.18f;
    }
    void StartAirborne()
    {
        animator.speed = 1;
        animator.Play("Jump");
    }
    private void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    //fa check si el objecte empty groundCheck fa overlap amb un objecte que pertany a la groundLayer (overlap amb radi de 0.2f)
    private bool CheckGround()
    {
        return Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundLayer).Length > 0;
    }
    //funció que permet mapejar un valor a dintre d'un rang que decideixes (generalment decidirem entre 0 i 1)
    public static float Map(float value, float min1, float max1, float min2, float max2, bool clamp = false)
    {
        float val = min2 + (max2 - min2) * ((value - min1) / (max1 - min1));
        return clamp ? Mathf.Clamp(val, Mathf.Min(min2, max2), Mathf.Max(min2, max2)) : val;
    }
}