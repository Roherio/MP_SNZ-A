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
    float oldPosition;
    bool isDetectingRange = false;
    void Start()
    { 
    }

    private void FixedUpdate()
    {
        oldPosition = transform.position.x;
    }
    void Update()
    {
       
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
        if (isDetectingRange)
        {
            IsChasing();

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
    void resetAnimations()
    {
        animator.SetBool("InDetectionRange", false);
        animator.SetBool("InIdle", false);
        animator.SetBool("InAttackRange", false);
    }
}
