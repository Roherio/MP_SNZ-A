using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointEnemyScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, SecretarioEnemyScript.dashDurationTimer);

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("player hit");
            Destroy(gameObject);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2);
    }
}
