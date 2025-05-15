using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerDrop : MonoBehaviour
{
    [SerializeField] float dropDamageValue;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            DamageLiora_Script.RecibirDamage(transform.position, dropDamageValue);

            //
            Destroy(gameObject);
            print("L'atac ha fet hit per " + dropDamageValue + " punts de mal! La vida actual de Liora és " + GameControl_Script.lifeLiora);
        }
        if (collision.CompareTag("Puddle"))
        {
            Destroy(gameObject);
        }
    }
    public void Destruir()
    {
        Destroy(gameObject);
    }
  
}