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
        if (collision.CompareTag("Parry"))
        {
            print("te han hecho parry crackkk");
            return;
        }
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
            print("L'atac ha fet hit per " + enemyAttackValue + " punts de mal! La vida actual de Liora és " + GameControl_Script.lifeLiora);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, showRadius);
    }
}