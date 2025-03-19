using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    protected float startTime;
    public float time => Time.time - startTime;
    
    protected Rigidbody2D rb;
    protected Animator animator;
    
    //groundLogic
    public float horizontal;
    public bool isGrounded;

    //Jump Logic
    public bool jumping;
    [SerializeField] public float jumpPower = 24f;

    //Dash Logic
    /*private bool canDash = true;
    private bool isDashing;
    private float dashPower = 18f;
    private float dashTime = 0.2f;
    private float dashCooldown = 0.2f;
    [SerializeField] TrailRenderer trailRenderer;*/

    //LedgeGrab Logic
    public bool isGrabbingLedge = false;

    //Climb Logic
    /* private bool isClimbing = false;*/

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }

    public virtual void Setup(Rigidbody2D _rb, Animator _animator, float _horizontal)
    {
        rb = _rb;
        animator = _animator;
        horizontal = _horizontal;
    }
}
