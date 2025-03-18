using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SecretarioEnemyScript : MonoBehaviour
{
    public Animator animator;
    [SerializeField] float moveSpeed;
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
                    IsChasing();
                }
                break;

            case EnemyBehaviour.PATROL:

                if (isDetectingRange)
                {
                    IsChasing();

                    return;
                }

                if (destinationPoint == 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    transform.position = Vector2.MoveTowards(transform.position, patrolPoint[0].position, moveSpeed * Time.deltaTime);
                    animator.SetBool("InDetectionRange", true);
                    if (Vector2.Distance(transform.position, patrolPoint[0].position) < 0.1)
                    {
                        destinationPoint = 1;
                    }
                }

                if (destinationPoint == 1)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    transform.position = Vector2.MoveTowards(transform.position, patrolPoint[1].position, moveSpeed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, patrolPoint[1].position) < 0.1)
                    {
                        destinationPoint = 0;

                    }
                }
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
    void IsChasing()
    {
        Behaviour = EnemyBehaviour.STANDING;
        if (Vector2.Distance(transform.position, playerPosition.position) < attackRange)
        {
            resetAnimations();
            animator.SetBool("InAttackRange", true);
            StartCoroutine(DashMovementHandler());
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPosition.position.x, transform.position.y), moveSpeed * Time.deltaTime);
            animator.SetBool("InDetectionRange", true);
        }
    }
    IEnumerator DashMovementHandler()
    {
        canDash = true;
        isPreparingForDash = true;

        yield return new WaitForSeconds(0.5f);
        isDashing = true;
        isPreparingForDash = false;

        yield return new WaitForSeconds(0.2f);
        canDash = false;
        isDashing = false;

        yield return new WaitForSeconds(4f);
        canDash = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private void dashToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPosition.position.x, transform.position.y), (moveSpeed * 5) * Time.deltaTime);
    }
    void resetAnimations()
    {
        animator.SetBool("InDetectionRange", false);
        animator.SetBool("InIdle", false);
        animator.SetBool("InAttackRange", false);
    }
}

