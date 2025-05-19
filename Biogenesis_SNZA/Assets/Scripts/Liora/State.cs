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
    public bool isDashing = false;
    //LedgeGrab Logic
    public bool isGrabbingLedge = false;
    //Climb Logic
    public bool isClimbing = false;
    //ACTIONS Logic
    public bool isBreakingWall = false;
    public bool isTakingItem = false;
    public bool isKnockedBack = false;
    public bool isDying = false;
    //attack Logic
    public bool isAttacking = false;
    public bool isParrying = false;
    public bool isDoingUlti = false;

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
