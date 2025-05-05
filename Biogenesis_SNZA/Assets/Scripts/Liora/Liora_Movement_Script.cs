using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Liora_Movement_Script : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerInput playerInput;
    //Ground Logic
    public BoxCollider2D groundCheck;
    public LayerMask groundLayer;

    //ROGER (TEST)
    public knockbackScript knockbackScript;


    //Jump Logic
    public float horizontal { get; private set; }
    public bool jumping;
    [SerializeField] float groundSpeed = 10f;
    [SerializeField] float jumpPower = 24f;

    //Dash Logic
    public bool isDashing = false;
    private bool canDash = true;
    [SerializeField] private float dashPower = 36f;
    private float dashTime = 0.2f;
    private float dashCooldown = 0.6f;
    private float stopMovementAfterDash;
    [SerializeField] TrailRenderer trailRenderer;

    //LedgeGrab Logic
    public static bool isGrabbingLedge = false;
    private bool canGrabLedge = true;
    private Vector2 ledgePosition;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private LayerMask ledgeLayer;
    [SerializeField] private float ledgeGrabOffsetY = -3.2f;

    //Climb Logic
    public bool isClimbing = false;
    //variable que controla si esta en rang d'una escala
    public static bool canClimb = false;
    [SerializeField] float climbSpeed = 6f;
    [SerializeField] private Transform climbCheck;
    [SerializeField] private LayerMask climbLayer;

    void Awake()
    {
        knockbackScript = GetComponent<knockbackScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameControl_Script.isPaused) { return; }
        if (knockbackScript != null && knockbackScript.isKnockedBack)
        {
            return; // Skip movement while in knockback
        }

        //pas de variables a la state machine
        Liora_StateMachine_Script.horizontal = horizontal;
        Liora_StateMachine_Script.isGrounded = CheckGround();
        Liora_StateMachine_Script.isGrabbingLedge = isGrabbingLedge;
        Liora_StateMachine_Script.isDashing = isDashing;
        Liora_StateMachine_Script.isClimbing = isClimbing;
        CheckForClimb();
        CheckForLedge();
        stopMovementAfterDash -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (GameControl_Script.isPaused) { return; }
        //amb aquest If evitem que el jugador pugui moure's si esta fent dash
        if (isDashing || (knockbackScript != null && knockbackScript.isKnockedBack)) { return; }
        //bloquejarem qualsevol moviment si el jugador esta agafat a un ledge o si està executant una ordre d'atac
        if (isGrabbingLedge || Liora_Attack_Script.isAttacking || Liora_Attack_Script.isParrying || Liora_Attack_Script.isDoingUlti /*|| stopMovementAfterDash > 0*/)
        {
            horizontal = 0f;
        }
        if (!isClimbing)
        {
            rb.velocity = new Vector2(horizontal * groundSpeed, rb.velocity.y);
        }
    }
    public void Movimiento(InputAction.CallbackContext context)
    {
        //if (isGrabbingLedge) { return; }
        horizontal = context.ReadValue<Vector2>().x;
    }
    public void Saltar(InputAction.CallbackContext context)
    {
        if (GameControl_Script.isPaused) { return; }
        //evitar que salti durant un dash o durant un atac/parry/ulti
        if (isDashing || isGrabbingLedge || Liora_Attack_Script.isAttacking || Liora_Attack_Script.isParrying || Liora_Attack_Script.isDoingUlti) { return; }
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
        if (GameControl_Script.isPaused) { return; }
        if (isDashing || isGrabbingLedge || Liora_Attack_Script.isAttacking || Liora_Attack_Script.isParrying || Liora_Attack_Script.isDoingUlti) { return; }
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
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        trailRenderer.emitting = true;
        //ignorar colisions amb enemies
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        yield return new WaitForSeconds(dashTime);
        stopMovementAfterDash = 0.5f;
        isDashing = false;
        trailRenderer.emitting = false;
        //retornar colisions amb enemies
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
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