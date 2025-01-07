using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    Rigidbody2D EnemyRigidbody;
    void Start()
    {
        EnemyRigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (FacingRight())
        {
            EnemyRigidbody.velocity  = new Vector2 (speed, 0f);
        }
        else
        {
            EnemyRigidbody.velocity = new Vector2(-speed, 0f);
        }
    }

    private bool FacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }


    //Detecta si deja de haber una pared o no, para saber si darse la vuelta
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Se da la vuelta
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
