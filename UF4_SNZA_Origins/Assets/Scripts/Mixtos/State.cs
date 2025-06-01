using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    //SCRIPT MIXTE adaptat per incorporar-lo al nostre projecte a partir d'una base apresa a internet. Canal de Youtube: AdamCYounis. Link al vídeo: https://www.youtube.com/watch?v=-jkT4oFi1vk (a partir del minut 18:00 explica com fer una classe abstracta de la qual heretaran qualitats altres classes més específiques com cada estat. Aquests altres estats utilitzaran funcions definides aqui com a virtual void)

    //script pare que defineix paràmetres pels seus scripts fills que hereten propietats com és cada estat particular de la màquina d'estats del personatge

    //fem isComplete com a get;protected set; perquè tothom pugui llegir el valor desde fora però només dintre de cada estat particular podem dir que isComplete = true
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
    public bool isTakingPotion = false;
    public bool isTakingItem = false;
    public bool isKnockedBack = false;
    public bool isDying = false;
    //attack Logic
    public bool isAttacking = false;
    public bool isShooting = false;

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }
    //funcio setup que farà que s'hagi de definir sobre quin rigidbody s'actua, quin animator i quin valor de velocitat horitzontal
    public virtual void Setup(Rigidbody2D _rb, Animator _animator, float _horizontal)
    {
        rb = _rb;
        animator = _animator;
        horizontal = _horizontal;
    }
}