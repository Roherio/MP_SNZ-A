using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Aquest Script controla els HP de TOTS el enemics, dins d'aquest mateix, podem decidir a quin enemic esta afectant
public class hpEnemiesScript : MonoBehaviour
{
    [SerializeField] private string nameEnemy;
    public float maxEnemyHP;
    public float enemyHP;
    public float deathAnimationDuration; //caldra determinar aixo a l'inspector depenent de cada prefab i la seva duracio de l'animació
    public Animator animator;

    SpriteRenderer spriteRenderer;
    private Color originalColor;

    public bool isDead = false;

    Rigidbody2D rb;
    ParentEnemy ParentEnemyScript;
    public BossMusicController bossMusic;
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
        if (collision.CompareTag("LioraAttack")) //Si el enemic fa collisio amb l'atac del Player
        {
            enemyHP = enemyHP - Liora_Attack_Script.damageAttackLiora; //treu vida
            //StartCoroutine(FlashWhite());
            spriteRenderer.color = Color.red;
            Invoke("ResetMaterial", 0.2f);
            print("has hecho dañito");
            if (enemyHP <= 0f) //Si el matem
            {
                isDead = true;
                GetComponent<BoxCollider2D>().enabled = false; //Aixi no colisiones amb l'enemic mentres fa l'animació de mort
                
                CinemachineShake.Instance.ShakeCamera(1f, .3f); //Li dona efecte al matar un enemic, fa que la camera es mogui
                FindObjectOfType<HitStop>().hitStop(0.05f); //També per afegir feeling, fa que es pari el joc, que doni la sensació que has donat un bon cop

                //reset de les animacion per si de cas
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
        SceneManager.LoadScene("TransicioFinal"); // Aixo només després del boss final
    }
    private IEnumerator FlashWhite()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(1f); // Duración del parpadeo
        spriteRenderer.color = originalColor;
    }
    public void finalFadeOut()
    {
        CameraFading fade = Camera.main.GetComponent<CameraFading>();
        fade.FadeOutSlow();
    }
}
