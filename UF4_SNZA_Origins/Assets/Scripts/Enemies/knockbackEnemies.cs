using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockbackScript : MonoBehaviour
{
    LioraAudioManager audioManagerLiora;

    public Rigidbody2D rb;
    public bool isKnockedBack = false;
    public float knockbackDuration = 0.5f;

    private void Start()
    {
        audioManagerLiora = GameObject.FindGameObjectWithTag("LioraAudioManager").GetComponent<LioraAudioManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector2 sourcePosition, float knockbackForce)
    {
        highDamageTaken();
        Vector2 direction = (rb.position - sourcePosition).normalized;
        rb.velocity = Vector2.zero; // Reset current velocity
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        Debug.DrawLine(rb.position, rb.position + direction * 2, Color.red, 1f);
        StartCoroutine(KnockbackCooldown());
    }

    private IEnumerator KnockbackCooldown()
    {
        //activar knockback, i mentres esta en knockback no et poden tornar a pegar fins que es desactivi
        isKnockedBack = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        //activem l'estat de la màquina d'estats "knock back" per mostrarla ferida a la personatge, i posem en fals l'estat isAttacking i isShooting perquè no tinguin prioritat a la màquina d'estats
        Liora_StateMachine_Script.isKnockedBack = true;
        Liora_Attack_Script.isAttacking = false;
        Liora_Attack_Script.isShooting = false;
        yield return new WaitForSeconds(knockbackDuration);
        //desactivar knockback i tornar a ser vulnerable
        isKnockedBack = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        Liora_StateMachine_Script.isKnockedBack = false;
    }
    public void highDamageTaken()
    {
        audioManagerLiora.LioraSFX(audioManagerLiora.voiceDamage);
        audioManagerLiora.LioraSFX(audioManagerLiora.damage);
    }
    public void lowDamageSFX()
    {
        audioManagerLiora.LioraSFX(audioManagerLiora.voiceDamage);
    }
}
