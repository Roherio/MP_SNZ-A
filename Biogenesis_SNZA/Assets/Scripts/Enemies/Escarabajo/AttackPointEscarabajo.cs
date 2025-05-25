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
        Destroy(gameObject, (EscarabajoEnemyScript.attackDurationTimer));
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
            /*
            //Knockback effect
            knockbackScript kb = collision.GetComponent<knockbackScript>();
            if (kb != null)
            {
                kb.ApplyKnockback(transform.position, enemyAttackValue);
            }
            */
            //Treu vida
            DamageLiora_Script.RecibirDamage(transform.position, enemyAttackValue);

            //
            print("L'atac ha fet hit per " + enemyAttackValue + " punts de mal! La vida actual de Liora és " + GameControl_Script.lifeLiora);
            Destroy(gameObject);
            
        }
        else
        {
            Invoke("Destruir", 0.5f);
        }
    }
    public void Destruir()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, showRadius);
    }
}