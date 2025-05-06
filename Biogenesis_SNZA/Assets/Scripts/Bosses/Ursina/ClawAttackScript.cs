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
        Destroy(gameObject, (UrsinaScript.attackDurationTimer));
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
            //Consola
            print("player hit");

            //Feeling
            FindObjectOfType<HitStop>().hitStop(0.01f);
            CinemachineShake.Instance.ShakeCamera(10f, .01f);

            //Knockback effect
            knockbackScript kb = collision.GetComponent<knockbackScript>();
            if (kb != null)
            {
                kb.ApplyKnockback(transform.position, enemyAttackValue);
            }

            //Treu vida
            GameControl_Script.lifeLiora -= enemyAttackValue;

            //
            Destroy(gameObject);
            print("Has tocat a l'enemic! T'ha fet " + enemyAttackValue +" punts de mal! La vida actual de Liora és " + GameControl_Script.lifeLiora);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, showRadius);
    }
}
