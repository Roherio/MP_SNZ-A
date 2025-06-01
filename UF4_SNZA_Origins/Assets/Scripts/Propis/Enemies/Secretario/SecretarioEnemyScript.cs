using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class SecretarioEnemyScript : MonoBehaviour
{
    [Header("--------------------Audio--------------------")]
    secretarioAudioManager audioManager;
    private bool hasPlayedDeathSound = false;
    public float soundDetectionCooldwon = 0f, soundCooldown = 10f;

    [Header("--------------------GameObjects--------------------")]
    public Animator animator;
    public Rigidbody2D rb;
    public Transform attackPoint;
    [SerializeField] GameObject attackCollision;
    [SerializeField] Transform playerPosition;
    [SerializeField] Transform[] patrolPoint;

    [Header("--------------------Movement variables--------------------")]
    [SerializeField] float moveSpeed;
    private float dashTimer = 0f;
    public static float dashDurationTimer;
    [SerializeField] float dashDuration, dashSpeed, dashCooldown, detectionRange;
    private Vector2 dashDirection;

    [Header("--------------------Attack variables--------------------")]
    [SerializeField] float attackRange;
    private bool canAttack = true, isAttacking = false;

    

   
    
    
    public int destinationPoint;
    [SerializeField] float verticalViewHeight = 3; 

    hpEnemiesScript hpEnemiesScript;

    public EnemyBehaviour Behaviour = EnemyBehaviour.STANDING;

    void Start()
    {
        patrolPoint[0] = transform.parent.Find("PatrolPoint (0)");
        patrolPoint[1] = transform.parent.Find("PatrolPoint (1)");
        audioManager = GameObject.FindGameObjectWithTag("SecretarioAudioManager").GetComponent<secretarioAudioManager>();
        hpEnemiesScript = GetComponent<hpEnemiesScript>();
        playerPosition = GameObject.FindWithTag("Player").transform;
        dashDurationTimer = dashDuration; //valor únicament creat per després ser portat a un altre script
        isAttacking = false;
        canAttack = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        float enemyToPlayerDistance = Vector2.Distance(transform.position, playerPosition.position); //float per veure la distancia en TOTES LES DIRECCIONS entre el jugador i l'enemic
        float verticalDetection = Mathf.Abs(playerPosition.position.y - transform.position.y); //float per crear un valor que és la diferència entre l'ALTURA del jugador i l'enemic

        switch (Behaviour)
        {
            case EnemyBehaviour.STANDING:

                if (hpEnemiesScript.isDead && !hasPlayedDeathSound)
                {
                    StopAllCoroutines();
                    audioManager.SecretarioSFX(audioManager.Death);
                    hasPlayedDeathSound = true;
                    if (rb != null)
                    {
                        rb.velocity = Vector2.zero;
                    }
                    
                    return;
                }
                if (isAttacking) return;

                if (enemyToPlayerDistance < attackRange && canAttack && verticalDetection < verticalViewHeight) //Prepara l'atac quan el jugador està suficientment aprop i en el seu rang de visió
                {
                    print("I can dash!");
                    isAttacking = true;
                    rb.velocity = Vector2.zero;
                    StartCoroutine(Dash());
                }
                else if (enemyToPlayerDistance <= detectionRange && verticalDetection < verticalViewHeight) //S'apropa al jugador per que estigui dins el rang d'atac i en el seu rang de visió
                {
                    print("im chasing");
                    isChasing();
                }
                else { rb.velocity = Vector2.zero; animator.SetBool("isWalking", false); } //Com l'estat es STANDING, es queda esperant a algun canvi
                break;

            case EnemyBehaviour.PATROL:
                if (hpEnemiesScript.isDead && !hasPlayedDeathSound)
                {
                    if (rb != null)
                    {
                        rb.velocity = Vector2.zero;
                    }
                    StopAllCoroutines();
                    audioManager.SecretarioSFX(audioManager.Death);
                    hasPlayedDeathSound = true;
                    return;
                }
                if (isAttacking) return;
                if (enemyToPlayerDistance < attackRange && canAttack && verticalDetection < verticalViewHeight) //Prepara l'atac quan el jugador està suficientment aprop i en el seu rang de visió
                {
                    print("I can dash!");
                    isAttacking = true;
                    rb.velocity = Vector2.zero;
                    StartCoroutine(Dash());
                }
                else if (enemyToPlayerDistance <= detectionRange && verticalDetection < verticalViewHeight) //S'apropa al jugador per que estigui dins el rang d'atac i en el seu rang de visió
                {
                    print("im chasing");
                    isChasing();
                }
                else 
                {
                    //Com l'estat es PATROL, si no detecta cap jugador comença a caminar entre dos punts
                    if (destinationPoint == 0)
                    {
                        animator.SetBool("isWalking", true);
                        transform.localScale = new Vector3(1, 1, 1);
                        transform.position = Vector2.MoveTowards(transform.position, patrolPoint[0].position, moveSpeed * Time.deltaTime);
                        if (Vector2.Distance(transform.position, patrolPoint[0].position) < 0.1)
                        {
                            destinationPoint = 1;
                        }
                    }

                    if (destinationPoint == 1)
                    {
                        animator.SetBool("isWalking", true);
                        transform.localScale = new Vector3(-1, 1, 1);
                        transform.position = Vector2.MoveTowards(transform.position, patrolPoint[1].position, moveSpeed * Time.deltaTime);
                        if (Vector2.Distance(transform.position, patrolPoint[1].position) < 0.1)
                        {
                            destinationPoint = 0;

                        }
                    }
                }
                
                break;
        }

    }

    void isChasing()
    {
        if (soundDetectionCooldwon > 0)
        {
            soundDetectionCooldwon -= Time.deltaTime;
        }
        TryPlayAlertSound();

        animator.SetBool("isWalking", true);
        Vector2 direction = playerPosition.position - transform.position; //Aqui fem un nou vector només per la direcció, els anteriors eren per saber la distancia o per saber l'altura relativa entre jugador i enemic.
        direction.y = 0; //fem que la direcció en y sigui 0
        direction = direction.normalized; //normalitzem el vector perque el valor sigui 1 o 0

        if (!isAttacking && canAttack)
        {
            if (rb != null)
            {
                rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y); //mou-te cap al jugador
            }
            
            if (direction.x < 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
        
    }

    IEnumerator Dash()
    {
        animator.SetBool("isWalking", false);
        
        //Temps de preparació de l'atack

        isAttacking = true;
        canAttack = false;
        if (playerPosition.position.x < transform.position.x) { transform.localScale = new Vector3(1, 1, 1); }
        else { transform.localScale = new Vector3(-1, 1, 1); }
        dashDirection = (playerPosition.position - transform.position);
        dashDirection.y = 0;
        dashDirection = dashDirection.normalized;
        dashTimer = 0f;
        

        yield return new WaitForSeconds(dashCooldown);
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(.3f);
        //Fa el dash/atac cap a l'enemic
        audioManager.SecretarioSFX(audioManager.Attack);

        attackHitbox();

        //Quan de temps dura el dash?
        while (dashTimer < dashDuration)
        {
            
            rb.velocity = dashDirection * dashSpeed;
            dashTimer += Time.deltaTime;
            yield return null;
        }

        //S'acaba el dash, aturat i posa't en cooldown
        rb.velocity = Vector2.zero; 
        
        animator.SetBool("isAttacking", false);

        yield return new WaitForSeconds(dashCooldown);
        animator.SetBool("isWalking", false);
        isAttacking = false;
        //Pot tornar a atacar
        if (playerPosition.position.x < transform.position.x) { transform.localScale = new Vector3(1, 1, 1); }
        else { transform.localScale = new Vector3(-1, 1, 1); }
        canAttack = true;
    }

    public void attackHitbox() //Genera la hitbox de l'atac només quan nosaltres volem
    {
        Instantiate(attackCollision, attackPoint.transform, worldPositionStays: false);
    }

    private void OnDrawGizmos() //Ajudes visuals
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Vector3 leftEdge = transform.position + Vector3.left * detectionRange;
        Vector3 rightEdge = transform.position + Vector3.right * detectionRange;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(leftEdge + Vector3.up * verticalViewHeight, rightEdge + Vector3.up * verticalViewHeight);
        Gizmos.DrawLine(leftEdge + Vector3.down * verticalViewHeight, rightEdge + Vector3.down * verticalViewHeight);
    }
    void TryPlayAlertSound()
    {
        if (soundDetectionCooldwon <= 0f)
        {
            audioManager.SecretarioSFX(audioManager.playerDetected);
            soundDetectionCooldwon = soundCooldown;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Treu vida
            DamageLiora_Script.RecibirDamage(transform.position, 5f);
        }
    }
    void OnEnable()
    {
        canAttack = true;
        isAttacking = false;
        playerPosition = GameObject.FindWithTag("Player").transform;
    }
}

