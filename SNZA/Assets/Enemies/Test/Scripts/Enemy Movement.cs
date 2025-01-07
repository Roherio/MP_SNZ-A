using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoint;
    public float moveSpeed;
    public int destinationPoint;

    public void flip()
    {
        transform.localScale = new Vector2((-transform.localScale.x), transform.localScale.y);
    }
    // Update is called once per frame
    void Update()
    {
        if (destinationPoint == 0)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, patrolPoint[0].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoint[0].position) < .2f)
            {
                flip();
                destinationPoint = 1;
                

            }

        }
        if (destinationPoint == 1)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, patrolPoint[1].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoint[1].position) < .2f)
            {
                destinationPoint = 0;
                transform.localScale = new Vector2((-transform.localScale.x), transform.localScale.y);
            }

        }
    }
}
