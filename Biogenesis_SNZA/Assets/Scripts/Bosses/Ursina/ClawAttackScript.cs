using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAttackScript : MonoBehaviour
{
    [SerializeField] float enemyAttackValue;
    [SerializeField] float showRadius;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, (UrsinaScript.attackDurationTimer + 0.5f));
        print(GameControl_Script.lifeLiora);
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
            GameControl_Script.lifeLiora -= enemyAttackValue;
            Destroy(gameObject);
            print(GameControl_Script.lifeLiora);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, showRadius);
    }
}
