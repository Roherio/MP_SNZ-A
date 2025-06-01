using System.Collections;
using UnityEngine;

//Aquest script empeny al jugador lluny l'enemic
public class RoarAttackScript : MonoBehaviour
{
    private Vector3 scaleChange;
    public float knockbackForce = 30f;

    void Start()
    {
        scaleChange = new Vector3(1f, 1f, 1f);
        Destroy(gameObject, 1f);

        int roarLayer = LayerMask.NameToLayer("RoarAttack");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(roarLayer, enemyLayer, true); //Si no fem aixo, tambe empeny al propi enemic
    }

    void Update()
    {
        transform.localScale += scaleChange * Time.deltaTime; //Basicament es una collisio que es va fent mes gran, llavors es una mica un "fals" empeny
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            knockbackScript kb = collision.GetComponent<knockbackScript>();
            if (kb != null)
            {
                kb.ApplyKnockback(transform.position, knockbackForce);
            }
        }
    }
}
