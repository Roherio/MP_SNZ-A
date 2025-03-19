using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Liora_Movement_Script : MonoBehaviour
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
    public BoxCollider2D groundCheck;
    public LayerMask groundLayer;

    //Jump Logic
    public float horizontal { get; private set; }
    public bool jumping;
    [SerializeField] float groundSpeed = 5f;
    [SerializeField] float jumpPower = 10f;

    //Dash Logic
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashPower = 36f;
    private float dashTime = 0.2f;
    private float dashCooldown = 0.2f;
    [SerializeField] TrailRenderer trailRenderer;

    //LedgeGrab Logic
    [SerializeField]private bool isGrabbingLedge = false;
    private bool canGrabLedge = true;
    private Vector2 ledgePosition;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private LayerMask ledgeLayer;
    [SerializeField] private float ledgeGrabOffsetY = -1.6f;

    //Climb Logic
    [SerializeField] private bool isClimbing = false;
    //variable que controla si esta en rang d'una escala
    public static bool canClimb = false;
    [SerializeField] float climbSpeed = 3f;
    [SerializeField] private Transform climbCheck;
    [SerializeField] private LayerMask climbLayer;

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
        state.isGrounded = CheckGround();
        state.isGrabbingLedge = isGrabbingLedge;
        //amb aquest If evitem que el jugador pugui moure's si esta fent dash
        if (isDashing) { return; }
        if (isGrabbingLedge)
        {
            horizontal = 0f;
        }
        if (!isClimbing)
        {
            rb.velocity = new Vector2(horizontal * groundSpeed, rb.velocity.y);
        }
        
        CheckForClimb();
        CheckForLedge();
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
    public void Movimiento(InputAction.CallbackContext context)
    {
        //if (isGrabbingLedge) { return; }
        horizontal = context.ReadValue<Vector2>().x;
    }
    public void Saltar(InputAction.CallbackContext context)
    {
        //evitar que salti durant un dash
        if (isDashing || isGrabbingLedge) { return; }
        if (context.started)
        {
            if (CheckGround() || isClimbing)
            {
                //saltarà amb la primera input (context.started, sinó seria input continu) nomes si isGrounded es vertader
                isClimbing = false;
                canClimb = false;
                Invoke("EnableClimb", 0.3f);
                rb.gravityScale = 6f;
                rb.velocity = new Vector2(rb.velocity.x * horizontal, jumpPower);
                jumping = true;
            }
            
        }
        if (context.canceled && rb.velocity.y > 0)
        {
            //si deixes de premer el botó (context.canceled) i està pujant encara (rb.velocity.y > 0), tallarà el salt fent que baixi la seva velocitat vertical a la meitat
            rb.AddForce(new Vector2(0, -8f), ForceMode2D.Impulse);
        }
    }
    public void Dash(InputAction.CallbackContext context)
    {
        if (context.started && CheckGround() == true && canDash == true)
        {
            StartCoroutine(Dash());
        }
    }
    public void LedgeInput(InputAction.CallbackContext context)
    {
        if (!isGrabbingLedge) { return; }
        if (context.started)
        {
            float inputY = context.ReadValue<Vector2>().y;
            if (inputY > 0)
            {
                ClimbLedge();
            }
            else if (inputY < 0)
            {
                DropFromLedge();
            }
        }
    }
    public void ClimbInput(InputAction.CallbackContext context)
    {
        if (!isClimbing) { return; }
        float inputY = context.ReadValue<Vector2>().y;
        float inputX = context.ReadValue<Vector2>().x;
        if (inputY != 0f)
        {
            rb.velocity = new Vector2(0f, inputY * climbSpeed);
        }
        else
        {
            rb.velocity = new Vector2 (0f, 0f);
        }
        rb.gravityScale = 0f;
    }
    void SelectState()
    {
        if (CheckGround() && Mathf.Abs(rb.velocity.y) < Mathf.Epsilon)
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
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        trailRenderer.emitting = true;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    private void ClimbLedge()
    {
        //evita multiples grabs i resetea gravity
        canGrabLedge = false;
        rb.gravityScale = 6f;
        isGrabbingLedge = false;
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);

        //una altra forma de pujar si fessim animació de pujada. En comptes d'això, potser saltar cap amunt i ja
        /*
        //Vector2 climbPosition = new Vector2(transform.position.x, transform.position.y + 1.2f);
        //yield return new WaitForSeconds(0.2f);
        //fer que el jugador pugi el ledge
        //transform.position = climbPosition;*/
        Invoke("EnableLedgeGrab", 0.2f);
    }
    private void DropFromLedge()
    {
        isGrabbingLedge = false;
        rb.gravityScale = 6f;
        //evitar multiples grabs
        canGrabLedge = false;
        //dropejar el player una mica
        Vector2 dropPosition = new Vector2(transform.position.x, transform.position.y - 1f);
        transform.position = dropPosition;
        //utilitzem invoke per cridar una funció despres d'un petit delay
        Invoke("EnableLedgeGrab", 0.5f);
    }
    private void EnableLedgeGrab()
    {
        canGrabLedge = true;
    }
    private void EnableClimb()
    {
        canClimb = true;
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
    private void CheckForClimb()
    {
        if (isClimbing || isGrabbingLedge || !canClimb) { return; }
        Collider2D wall = Physics2D.OverlapCircle(climbCheck.position, 0.2f, climbLayer);
        if (wall != null)
        {
            isClimbing = true;
            jumping = false;
            //parar moviment
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
        }
        else
        {
            isClimbing = false;
            rb.gravityScale = 6f;
            if (!isGrabbingLedge)
            {
                rb.gravityScale = 6f;
            }
        }
    }
    //funció que comprova si estem en range de un ledge
    private void CheckForLedge()
    {
        if (isClimbing || isGrabbingLedge || CheckGround()) { return; }
        Collider2D ledge = Physics2D.OverlapCircle(ledgeCheck.position, 0.2f, ledgeLayer);
        if(ledge != null && canGrabLedge)
        {
            isGrabbingLedge = true;
            jumping = false;
            //parar el moviment
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            //fer snap al ledge perfecte
            ledgePosition = new Vector2(transform.position.x, ledge.transform.position.y + ledgeGrabOffsetY);
            transform.position = ledgePosition;
        }
    }
}