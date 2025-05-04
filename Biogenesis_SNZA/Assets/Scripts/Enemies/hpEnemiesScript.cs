using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpEnemiesScript : MonoBehaviour
{
    public float enemyHP;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        // Obtener el SpriteRenderer al iniciar
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LioraAttack"))
        {
            enemyHP -= enemyHP - Liora_Attack_Script.damageAttackLiora;
            StartCoroutine(FlashWhite());
            FindObjectOfType<HitStop>().hitStop(0.005f);
            CinemachineShake.Instance.ShakeCamera(5f, .3f);

            if (enemyHP <= 0f) { Destroy(gameObject); }
        }

    }
    private IEnumerator FlashWhite()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f); // Duración del parpadeo
        spriteRenderer.color = originalColor;
    }
}
