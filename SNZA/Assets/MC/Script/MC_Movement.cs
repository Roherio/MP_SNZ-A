using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Movement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpPower = 16f;
    private bool isFacingRight = true;
    Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isRunning", false);
    }

    // Update is called once per frame
    void Update()
    {
        //per utilitzar WASD o arrowkeys
        horizontal = Input.GetAxisRaw("Horizontal");
        //si pulses jump i toques terra saltes
        IsGround();
        if (Input.GetButtonDown("Jump") && IsGround())
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                animator.SetBool("isJumping", true);
            }
            //si pulses jump I ESTAS ANANT CAP AMUNT, prolongaràs una mica el salt
            if (Input.GetButtonDown("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
        /*if(Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            animator.SetBool("isJumping", true);
        }
        //si pulses jump I ESTAS ANANT CAP AMUNT, prolongaràs una mica el salt
        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }*/
        Flip();
        StateMachine();

    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private bool IsGround()
    {
        //crea cercle al voltant del player, si fa overlap amb el ground, permet saltar
        animator.SetBool("isJumping", false);
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
    //canviar de direcció si es dona una velocitat horitzontal contraria a la que està mirant (per exemple, si mires dreta i pulses tecla esquerra, donant velocitat -5f)

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void StateMachine()
    {
        if (IsGround())
        {
            animator.SetBool("isJumping", false);
            if (horizontal != 0f)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
        else if (!IsGround())
        {
            animator.SetBool("isJumping", true);
        }
        /*if (horizontal != 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }*/
    }
}