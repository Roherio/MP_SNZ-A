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

    private float dashTimer = 0f;
    public static float dashDurationTimer;
    [SerializeField] float dashDuration;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashCooldown;
    
    private Vector2 dashDirection;

    [SerializeField] float detectionRange;
    [SerializeField] float attackRange;

    private bool canAttack = true;
    private bool isAttacking = false;


    public Transform attackPoint;
    [SerializeField] GameObject attackCollision;

    [SerializeField] Transform playerPosition;
    [SerializeField] Transform[] patrolPoint;
    public int destinationPoint; 
    float oldPosition;


    public EnemyBehaviour Behaviour = EnemyBehaviour.STANDING;

    void Start()
    {
        playerPosition = GameObject.FindWithTag("Player").transform;
        dashDurationTimer = dashDuration;
        canAttack = true;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        oldPosition = transform.position.x;
    }
    void Update()
    {
        float verticalDetection = Mathf.Abs(playerPosition.position.y - transform.position.y);
        switch (Behaviour)
        {
            case EnemyBehaviour.STANDING:
                if (isAttacking) return;
                if (Vector2.Distance(transform.position, playerPosition.position) < attackRange && canAttack) // Dash if close enough
                {
                    print("I can dash!");
                    isAttacking = true;
                    rb.velocity = Vector2.zero;
                    StartCoroutine(Dash());
                }
                else if (Vector2.Distance(transform.position, playerPosition.position) <= detectionRange)
                {
                    print("im chasing");
                    isChasing();
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
                break;

            case EnemyBehaviour.PATROL:
                break;
        }

    }

    void isChasing()
    {
        Vector2 direction = playerPosition.position - transform.position;
        direction.y = 0;
        direction = direction.normalized;

        if (!isAttacking && canAttack)
        {
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
            if (direction.x < 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    IEnumerator Dash()
    {
        isAttacking = true;
        canAttack = false;
        yield return new WaitForSeconds(dashCooldown); //preparación dash


        dashDirection = (playerPosition.position - transform.position);
        dashDirection.y = 0; //evita que dashee hacia arriba
        dashDirection = dashDirection.normalized;
        dashTimer = 0f;

        attackHitbox();
        while (dashTimer < dashDuration)
        {
            
            rb.velocity = dashDirection * dashSpeed;
            dashTimer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero; // Stop movement after dash
        isAttacking = false;

        yield return new WaitForSeconds(dashCooldown);

        if (playerPosition.position.x < transform.position.x) { transform.localScale = new Vector3(1, 1, 1); }
        else { transform.localScale = new Vector3(-1, 1, 1); }
            
        
   
            
            
        canAttack = true;
    }

    public void attackHitbox()
    {
        Instantiate(attackCollision, attackPoint.transform, worldPositionStays: false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        float verticalTolerance = 2.5f;
        Vector3 leftEdge = transform.position + Vector3.left * detectionRange;
        Vector3 rightEdge = transform.position + Vector3.right * detectionRange;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(leftEdge + Vector3.up * verticalTolerance, rightEdge + Vector3.up * verticalTolerance);
        Gizmos.DrawLine(leftEdge + Vector3.down * verticalTolerance, rightEdge + Vector3.down * verticalTolerance);
    }
    void resetAnimations()
    {
        animator.SetBool("InDetectionRange", false);
        animator.SetBool("InIdle", false);
        animator.SetBool("InAttackRange", false);
    }
}

