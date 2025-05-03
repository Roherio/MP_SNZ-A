using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashAttackScript : MonoBehaviour
{

    [SerializeField] Transform playerPosition;
    
    [SerializeField] float enemyAttackValue;

    public Rigidbody2D rb;
    private Vector2 attackDirection;
    [SerializeField] float spreadSpeed;
    private Vector3 scaleChange;

    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(1.5f, 1.5f, 1.5f);
        rb = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindWithTag("Player").transform;
        Destroy(gameObject,1.5f);
        print(GameControl_Script.lifeLiora);
        attackDirection = (playerPosition.position - transform.position);
        attackDirection.y = 0;
        attackDirection = attackDirection.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = attackDirection.normalized * spreadSpeed;
        transform.localScale += scaleChange * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("player hit");
            GameControl_Script.lifeLiora -= enemyAttackValue;
            print(GameControl_Script.lifeLiora);
        }

    }
}
