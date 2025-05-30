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
            //print("Has tocat a l'enemic! T'ha fet " + enemyAttackValue +" punts de mal! La vida actual de Liora és " + GameControl_Script.lifeLiora);
        }

    }
    private void OnDrawGizmos() //Aixo ens serveix per veure l'area atac en l'inspector
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, showRadius);
    }
}
