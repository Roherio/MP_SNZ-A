using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clasIceSpikesScript : MonoBehaviour
{
    [SerializeField] Transform playerPosition;
    [SerializeField] float enemyAttackValue;
    public Rigidbody2D rb;
    private Vector2 attackDirection;
    [SerializeField] float objectSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindWithTag("Player").transform;
        Destroy(gameObject, 2f);
        print(GameControl_Script.lifeLiora);
        attackDirection = (playerPosition.position - transform.position);
        attackDirection.y = 0;
        attackDirection = attackDirection.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = attackDirection.normalized * objectSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Consola
            print("player hit");

            //Feeling
            FindObjectOfType<HitStop>().hitStop(0.01f);
            CinemachineShake.Instance.ShakeCamera(10f, .01f);


            //Treu vida
            DamageLiora_Script.RecibirDamage(transform.position, enemyAttackValue);

            //
            Destroy(gameObject);
            //print("L'atac ha fet hit per " + enemyAttackValue + " punts de mal! La vida actual de Liora �s " + GameControl_Script.lifeLiora);
        }

    }
}
