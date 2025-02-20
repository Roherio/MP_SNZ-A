using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_Liora_Movement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] float groundSpeed = 5f;
    [SerializeField] float jumpPower = 10f;
    public BoxCollider2D groundCheck;
    public LayerMask groundLayer;
    [SerializeField] private bool isGrounded;
    private bool jumping;
    Vector2 movim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(movim.x * groundSpeed, rb.velocity.y);
        StateMachine();
        FlipSprite();
        //fa check si el objecte empty groundCheck fa overlap amb un objecte que pertany a la groundLayer (overlap amb radi de 0.2f)
        CheckGround();
    }

    public void Movimiento(InputAction.CallbackContext context)
    {
        //print("moviendosee");
        movim = context.ReadValue<Vector2>();
    }
    public void Saltar(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            //saltarà amb la primera input (context.started, sinó seria input continu) nomes si isGrounded es vertader
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            //animació jump
            jumping = true;
        }
        if (context.canceled && rb.velocity.y > 0)
        {
            //si deixes de premer el botó (context.canceled) i està pujant encara (rb.velocity.y > 0), tallarà el salt fent que baixi la seva velocitat vertical a la meitat
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    public void StateMachine()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", false);
        if (isGrounded && rb.velocity.x != 0)
        {
            animator.SetBool("isRunning", true);
        }
        if (isGrounded && jumping)
        {
            animator.SetBool("isJumping", true);
        }
        if (!isGrounded && rb.velocity.y < 0)
        {
            animator.SetBool("isFalling", true);
        }
    }
    public void FlipSprite()
    {
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }
    public void CheckGround()
    {
        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundLayer).Length > 0;
    }
}