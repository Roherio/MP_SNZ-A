using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_Liora_Movement : MonoBehaviour
{
    Rigidbody2D rb;
    public BoxCollider2D groundCheck;
    public LayerMask groundLayer;

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
        StateMachine();
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
    public void StateMachine()
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
}