using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointEscarabajo : MonoBehaviour
{
    [SerializeField] float enemyAttackValue;
    [SerializeField] float showRadius;

    // Start is called before the first frame update
    void Start()
    {
        print("Collision spawned");
        Destroy(gameObject, (EscarabajoEnemyScript.attackDurationTimer));
        print(GameControl_Script.lifeLiora);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerStay2D(Collider2D collision)
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