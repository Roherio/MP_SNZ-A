using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockbackScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool isKnockedBack = false;
    public float knockbackDuration = 0.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector2 sourcePosition, float knockbackForce)
    {
        Vector2 direction = (rb.position - sourcePosition).normalized;
        rb.velocity = Vector2.zero; // Reset current velocity
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        Debug.DrawLine(rb.position, rb.position + direction * 2, Color.red, 1f);
        StartCoroutine(KnockbackCooldown());
    }

    private IEnumerator KnockbackCooldown()
    {
        isKnockedBack = true;
        Liora_StateMachine_Script.isKnockedBack = true;
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
        Liora_StateMachine_Script.isKnockedBack = false;
    }
}
