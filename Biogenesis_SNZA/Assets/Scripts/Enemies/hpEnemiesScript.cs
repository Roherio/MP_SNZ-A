using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hpEnemiesScript : MonoBehaviour
{
    [SerializeField] private string nameEnemy;
    public float maxEnemyHP;
    public float enemyHP;
    public float deathAnimationDuration; //caldra determinar aixo a l'inspector depenent de cada prefab i la seva duracio de l'animaci�
    public Animator animator;

    SpriteRenderer spriteRenderer;
    private Color originalColor;

    public bool isDead = false;

    Rigidbody2D rb;
    ParentEnemy ParentEnemyScript;
    public BossMusicController bossMusic;
    EscarabajoEnemyScript escarabajoEnemyScript;
    SecretarioEnemyScript secretarioEnemyScript;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ParentEnemyScript = GetComponentInParent<ParentEnemy>();
        enemyHP = maxEnemyHP;
        // Obtener el SpriteRenderer al iniciar
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        if (nameEnemy == "Jabali")
        {
            deathAnimationDuration = 1.2f;
        }

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
            //StartCoroutine(FlashWhite());
            spriteRenderer.color = Color.red;
            Invoke("ResetMaterial", 0.2f);
            print("has hecho da�ito");
            if (enemyHP <= 0f)
            {
                isDead = true;
                GetComponent<BoxCollider2D>().enabled = false;
                
                CinemachineShake.Instance.ShakeCamera(1f, .3f);
                FindObjectOfType<HitStop>().hitStop(0.05f);
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isDying", true);
                //un cop passats a falsos els altres estats porsiacaso, fem trigger de la variable isDying, i fem destroy en invoke per ferho en el temps que l'animacio dura
                if (nameEnemy == "Jabali")
                {
                    Destroy(rb);
                    ParentEnemyScript.enemyDead = true;
                    SNZAProgressControl_Script.bKilledJabali = true;
                }
                if (nameEnemy == "Secretario")
                {
                    Destroy(rb);
                    ParentEnemyScript.enemyDead = true;
                    SNZAProgressControl_Script.bKilledSecretario = true;
                }
                if (nameEnemy == "Ursina")
                {
                    rb.isKinematic = true;
                    bossMusic.bossDefeated = true;
                    Invoke("finalFadeOut", deathAnimationDuration - 1.5f);
                    Invoke("CargarFinalJuego", deathAnimationDuration);

                }
                Invoke("DestruirGameObject", deathAnimationDuration);
            }
        }

    }
    private void DestruirGameObject()
    {
        Destroy(gameObject);
    }
    void ResetMaterial()
    {
        spriteRenderer.color = originalColor;
    }
    void CargarFinalJuego()
    {
        SceneManager.LoadScene("TransicioFinal");
    }
    private IEnumerator FlashWhite()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(1f); // Duraci�n del parpadeo
        spriteRenderer.color = originalColor;
    }
    public void finalFadeOut()
    {
        CameraFading fade = Camera.main.GetComponent<CameraFading>();
        fade.FadeOutSlow();
    }
}
