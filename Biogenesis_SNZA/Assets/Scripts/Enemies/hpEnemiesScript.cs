using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpEnemiesScript : MonoBehaviour
{
    [SerializeField] private string nameEnemy;
    public float maxEnemyHP;
    public float enemyHP;
    public float deathAnimationDuration; //caldra determinar aixo a l'inspector depenent de cada prefab i la seva duracio de l'animació
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public bool isDead = false;

    ParentEnemy ParentEnemyScript;
    public BossMusicController bossMusic;
    EscarabajoEnemyScript escarabajoEnemyScript;
    SecretarioEnemyScript secretarioEnemyScript;
    private void Start()
    {
        ParentEnemyScript = GetComponentInParent<ParentEnemy>();
        enemyHP = maxEnemyHP;
        // Obtener el SpriteRenderer al iniciar
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LioraAttack"))
        {
            enemyHP = enemyHP - Liora_Attack_Script.damageAttackLiora;
            StartCoroutine(FlashWhite());
            FindObjectOfType<HitStop>().hitStop(0.005f);
            CinemachineShake.Instance.ShakeCamera(1f, .3f);
            print("has hecho dañito");
            if (enemyHP <= 0f)
            {
                isDead = true;
                
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isDying", true);
                //un cop passats a falsos els altres estats porsiacaso, fem trigger de la variable isDying, i fem destroy en invoke per ferho en el temps que l'animacio dura
                if (nameEnemy == "Jabali")
                {
                    ParentEnemyScript.enemyDead = true;
                    SNZAProgressJabali_Script.bKilledJabali = true;
                }
                if (nameEnemy == "Secretario")
                {
                    ParentEnemyScript.enemyDead = true;
                    SNZAProgressSecretario_Script.bKilledSecretario = true;
                }
                if (nameEnemy == "Ursina")
                {
                    bossMusic.bossDefeated = true;
                }
                Invoke("DestruirGameObject", deathAnimationDuration);
            }
        }

    }
    private void DestruirGameObject()
    {
        Destroy(gameObject);
    }
    private IEnumerator FlashWhite()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(1f); // Duración del parpadeo
        spriteRenderer.color = originalColor;
    }
}
