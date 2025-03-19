using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SecretarioEnemyScript : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;


    [SerializeField] float moveSpeed;

    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldown;
    [SerializeField] bool canDash;
    private float dashTimer = 0f;
    private Vector2 dashDirection;


    [SerializeField] float attackRange;
    [SerializeField] Collider2D DetectionRange;
    [SerializeField] Transform playerPosition;
    [SerializeField] Transform[] patrolPoint;
    public int destinationPoint;

    float oldPosition;
    bool isDetectingRange = false;
    public EnemyBehaviour Behaviour = EnemyBehaviour.STANDING;

    void Start()
    {
        canDash = true;
        rb = GetComponent<Rigidbody2D>();
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
                if (isDetectingRange)
                {
                    if (Vector2.Distance(transform.position, playerPosition.position) < attackRange && canDash) // Dash if close enough
                    {
                        print("I can dash!");
                        StartCoroutine(Dash());
                    }
                    else
                    {
                        animator.SetBool("InDetectionRange", true);
                        //transform.position = Vector2.MoveTowards(transform.position, new UnityEngine.Vector2(playerPosition.position.x, transform.position.y), moveSpeed * Time.deltaTime);
                    }
                }


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

    IEnumerator Dash()
    {
        canDash = false;
        dashDirection = (playerPosition.position - transform.position).normalized;

        dashTimer = 0f;
        while (dashTimer < dashDuration)
        {
            rb.velocity = dashDirection * dashSpeed;
            dashTimer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero; // Stop movement after dash

        yield return new WaitForSeconds(dashCooldown);
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

