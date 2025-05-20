using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerDrop : MonoBehaviour
{
    [SerializeField] float dropDamageValue;
    public Animator animator;
    private Rigidbody2D rb;
 

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        animator = GetComponent<Animator>();    
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
            Destruir();
        }
    }
    public void Destruir()
    {
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0;
        animator.SetBool("hasImpacted", true);
        Destroy(gameObject,0.2f);
    }
  
}