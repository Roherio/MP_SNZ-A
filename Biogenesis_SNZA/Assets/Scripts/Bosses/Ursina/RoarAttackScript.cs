using System.Collections;
using UnityEngine;

public class RoarAttackScript : MonoBehaviour
{
    private Vector3 scaleChange;

    void Start()
    {
        scaleChange = new Vector3(1f, 1f, 1f);
        Destroy(gameObject, 1f);

        int roarLayer = LayerMask.NameToLayer("RoarAttack");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(roarLayer, enemyLayer, true);
    }

    void Update()
    {
        transform.localScale += scaleChange * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            knockbackScript kb = collision.GetComponent<knockbackScript>();
            if (kb != null)
            {
                kb.ApplyKnockback(transform.position);
            }
        }
    }
}
