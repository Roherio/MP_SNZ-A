using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Animator animator;
    [SerializeField] float moveSpeed;
    [SerializeField] float detectionRange;
    [SerializeField] float attackRange;
    [SerializeField] Transform playerPosition;

    void Update()
    {
        if (Vector2.Distance(transform.position, playerPosition.position) < detectionRange)
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
        }
    }
    void IsChasing()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, moveSpeed * Time.deltaTime);
    }
    void resetAnimations()
    {
        animator.SetBool("InDetectionRange", false);
        animator.SetBool("InIdle", false);
        animator.SetBool("InAttackRange", false);
    }
}
