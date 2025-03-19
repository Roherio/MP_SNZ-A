using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SecretarioEnemyScript : MonoBehaviour
{
    public Animator animator;
    [SerializeField] float moveSpeed;
    private float originalSpeed;
    [SerializeField] Collider2D DetectionRange;
    [SerializeField] float attackRange;
    [SerializeField] Transform playerPosition;
    private bool canDash;
    private bool isPreparingForDash;
    private bool isDashing;

    [SerializeField] Transform[] patrolPoint;
    public int destinationPoint;


    float oldPosition;


    bool isDetectingRange = false;
    public EnemyBehaviour Behaviour = EnemyBehaviour.STANDING;

    void Start()
    {
        originalSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        oldPosition = transform.position.x;
    }
    void Update()
    {
        switch (Behaviour)
        {
            case EnemyBehaviour.STANDING:

                break;

            case EnemyBehaviour.PATROL:
                break;
        }
    
    }

    private void OnTriggerStay2D(Collider2D DetectionRange)
    {
        if (DetectionRange.CompareTag("Player"))
        {
            if (transform.position.x < playerPosition.position.x) { transform.localScale = new Vector3(-1, 1, 1); }
            if (transform.position.x > playerPosition.position.x) { transform.localScale = new Vector3(1, 1, 1); }
            isDetectingRange = true;
        }

    }
    private void OnTriggerExit2D(Collider2D DetectionRange)
    {
        if (DetectionRange.CompareTag("Player"))
        {
            isDetectingRange = false;
            resetAnimations();
        }

    }
    IEnumerator DashMovementHandler()
    {
        print("I'm preparing to dash!");
        canDash = true;
        isPreparingForDash = true;


        yield return new WaitForSeconds(2f);
        print("I'm dashing!");
        isDashing = true;
        isPreparingForDash = false;

        yield return new WaitForSeconds(1f);
        print("cooldown");
        canDash = false;
        isDashing = false;

        yield return new WaitForSeconds(4f);
        print ("I can dash again");
        canDash = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    void resetAnimations()
    {
        animator.SetBool("InDetectionRange", false);
        animator.SetBool("InIdle", false);
        animator.SetBool("InAttackRange", false);
    }
}

