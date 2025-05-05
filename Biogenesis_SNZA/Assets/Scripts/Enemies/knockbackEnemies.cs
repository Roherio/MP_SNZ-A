using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockbackScript : MonoBehaviour
{
    public float knockbackForce = 30f;
    public Rigidbody2D rb;
    public bool isKnockedBack = false;
    public float knockbackDuration = 0.2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector2 sourcePosition)
    {
        //Vector2 direction = new Vector2(rb.position.x - sourcePosition.x, 0).normalized;
        Vector2 direction = (rb.position - sourcePosition).normalized;
        //float xDirection = Mathf.Sign(rb.position.x - sourcePosition.x); // +1 or -1
        //Vector2 force = new Vector2(xDirection * knockbackForce, 0);

        rb.velocity = Vector2.zero; // Reset current velocity
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        Debug.DrawLine(rb.position, rb.position + direction * 2, Color.red, 1f);
        //rb.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(KnockbackCooldown());
    }

    private IEnumerator KnockbackCooldown()
    {
        isKnockedBack = true;
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
    }
}
