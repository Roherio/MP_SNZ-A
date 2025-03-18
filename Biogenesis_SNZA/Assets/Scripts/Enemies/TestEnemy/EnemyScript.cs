using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class EnemyScript : MonoBehaviour
{
    public Animator animator;
    [SerializeField] float moveSpeed;
    [SerializeField] Collider2D DetectionRange;
    [SerializeField] float attackRange;
    [SerializeField] Transform playerPosition;


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
        /*if (Vector2.Distance(transform.position, playerPosition.position) < detectionRange)
        {

            if (Vector2.Distance(transform.position, playerPosition.position) < attackRange)
            {
                resetAnimations();
                animator.SetBool("InAttackRange", true);
            }
            else
            {
                resetAnimations();
                IsChasing();
                animator.SetBool("InDetectionRange", true);
            }
        }

        else
        {
            resetAnimations();
        }*/  
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
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPosition.position.x, transform.position.y), moveSpeed * Time.deltaTime);
            animator.SetBool("InDetectionRange", true);
        }
    }
    /*IEnumerator flipDelay()
    {
        yield return new WaitForSeconds(2f);
        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        StartCoroutine(flipDelay());
    }*/
    void resetAnimations()
    {
        animator.SetBool("InDetectionRange", false);
        animator.SetBool("InIdle", false);
        animator.SetBool("InAttackRange", false);
    }
}
