using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAguilaProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;

    [SerializeField] float enemyAttackValue = 20f;
    [SerializeField] float y_Offset = 2;
    Vector3 playerCenter;
    [SerializeField] float projectileSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        playerCenter = player.transform.position + new Vector3(0, y_Offset, 0);

        Vector3 directionToPlayer = playerCenter - transform.position;
        rb.velocity = new Vector3(directionToPlayer.x, directionToPlayer.y, 0).normalized * projectileSpeed;

        float projectileRotation = Mathf.Atan2(-directionToPlayer.y, -directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, projectileRotation + 90);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("player hit");
            GameControl_Script.lifeLiora -= enemyAttackValue;
            Destroy(gameObject);
            print(GameControl_Script.lifeLiora);
        }
    }
}
