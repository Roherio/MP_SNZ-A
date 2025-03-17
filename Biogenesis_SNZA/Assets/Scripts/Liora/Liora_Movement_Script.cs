using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Liora_Movement_Script : MonoBehaviour
{
    private bool isFacingRight = true;
    Rigidbody2D rb;
    //Ground Logic
    public BoxCollider2D groundCheck;
    public LayerMask groundLayer;

    //StateMachine Logic
    enum PlayerState { Idle, Running, Airborne, LedgeGrabbing}
    PlayerState playerState;
    bool stateComplete;
    Animator animator;
    
    //Jump Logic
    private float horizontal;
    public bool jumping;
    [SerializeField] float groundSpeed = 5f;
    [SerializeField] float jumpPower = 10f;

    //Dash Logic
    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 18f;
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
    [SerializeField] float climbSpeed = 3f;
    //private bool canClimb = true;
    [SerializeField] private Transform climbCheck;
    [SerializeField] private LayerMask climbLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //amb aquest If evitem que el jugador pugui moure's si esta fent dash
        if (isDashing) { return; }
        /*if (CheckGround()) { jumping = false; }
        if (rb.velocity.y > 0) { canGrabLedge = false; }
        if (rb.velocity.y <= 0) { canGrabLedge = true; }*/
        rb.velocity = new Vector2(horizontal * groundSpeed, rb.velocity.y);
        if (!isFacingRight && horizontal > 0f)
        {
            FlipSprite();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            FlipSprite();
        }
        if (!isClimbing && !CheckGround())
        {
            //entrarà si el personatge no esta ja agafat a un objecte escalable
            CheckForClimb();
        }
        if (!isGrabbingLedge && rb.velocity.y < 0f && !CheckGround())
        {
            //entrara a aquest if si el personatge no està agafantse ja a un Ledge i la seva velocitat vertical està caient
            CheckForLedge();
        }
        if (stateComplete)
        {
            SelectState();
        }
        UpdateState();
    }
    public void Movimiento(InputAction.CallbackContext context)
    {
        if (isGrabbingLedge) { return; }
        horizontal = context.ReadValue<Vector2>().x;
    }
    public void Saltar(InputAction.CallbackContext context)
    {
        //evitar que salti durant un dash
        if (isDashing || isGrabbingLedge) { return; }
        if (context.started && CheckGround())
        {
            //saltarà amb la primera input (context.started, sinó seria input continu) nomes si isGrounded es vertader
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumping = true;
        }
        if (context.canceled && rb.velocity.y > 0)
        {
            //si deixes de premer el botó (context.canceled) i està pujant encara (rb.velocity.y > 0), tallarà el salt fent que baixi la seva velocitat vertical a la meitat
            rb.AddForce(new Vector2(0, -4f), ForceMode2D.Impulse);
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
        isClimbing = true;
        jumping = false;
        float inputY = context.ReadValue<Vector2>().y;
        rb.velocity = new Vector2(0f, inputY * climbSpeed);
    }
    void SelectState()
    {
        stateComplete = false;
        if (isGrabbingLedge == true)
        {
            playerState = PlayerState.LedgeGrabbing;
            StartLedgeGrabbing();
            return;
        }
        
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
        /*if (isGrabbingLedge == true)
        {
            playerState = PlayerState.LedgeGrabbing;
            StartLedgeGrabbing();
        }
        if (!CheckGround() && jumping == true)
        {
            playerState = PlayerState.Airborne;
            StartAirborne();
        }*/
    }
    void UpdateState()
    {
        if (isDashing) { return; }
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
            case PlayerState.LedgeGrabbing:
                UpdateLedgeGrabbing();
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
        if (CheckGround() || isGrabbingLedge == true)
        {
            jumping = false;
            stateComplete = true;
        }
    }
    void UpdateLedgeGrabbing()
    {
        if (!isGrabbingLedge)
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
    void StartLedgeGrabbing()
    {
        jumping = false;
        animator.speed = 1;
        animator.Play("Ledge");
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
        rb.gravityScale = 3f;
        isGrabbingLedge = false;
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);

        //una altra forma de pujar si fessim animació de pujada. En comptes d'això, potser saltar cap amunt i ja
        /*
        //Vector2 climbPosition = new Vector2(transform.position.x, transform.position.y + 1.2f);
        //yield return new WaitForSeconds(0.2f);
        //fer que el jugador pugi el ledge
        //transform.position = climbPosition;*/
        Invoke("EnableLedgeGrab", 0.2f);
        //stateComplete = true;
    }
    private void DropFromLedge()
    {
        isGrabbingLedge = false;
        rb.gravityScale = 3f;
        //evitar multiples grabs
        canGrabLedge = false;
        //dropejar el player una mica
        Vector2 dropPosition = new Vector2(transform.position.x, transform.position.y - 0.5f);
        transform.position = dropPosition;
        //utilitzem invoke per cridar una funció despres d'un petit delay
        Invoke("EnableLedgeGrab", 0.5f);
    }
    private void EnableLedgeGrab()
    {
        canGrabLedge = true;
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
        Collider2D wall = Physics2D.OverlapCircle(climbCheck.position, 0.2f, climbLayer);
        if (wall != null)
        {
            print("tas escalando");
            isClimbing = true;
            jumping = false;
            //parar moviment
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
        }
        /*else
        {
            rb.gravityScale = 3f;
        }*/
    }
    //funció que comprova si estem en range de un ledge
    private void CheckForLedge()
    {
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
    //funció que permet mapejar un valor a dintre d'un rang que decideixes (generalment decidirem entre 0 i 1)
    public static float Map(float value, float min1, float max1, float min2, float max2, bool clamp = false)
    {
        float val = min2 + (max2 - min2) * ((value - min1) / (max1 - min1));
        return clamp ? Mathf.Clamp(val, Mathf.Min(min2, max2), Mathf.Max(min2, max2)) : val;
    }
}