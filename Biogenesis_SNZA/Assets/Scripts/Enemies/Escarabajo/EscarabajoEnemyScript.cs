using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EscarabajoEnemyScript : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    private Transform playerPosition;

    [SerializeField] float moveSpeed;
    private float attackTimer = 0f;
    public static float attackDurationTimer;
    [SerializeField] float attackDuration;
    [SerializeField] float attackCooldown;
    private Vector2 dashDirection;
    [SerializeField] float dashSpeed;
    [SerializeField] float detectionRange;
    [SerializeField] float attackRange;
    private bool canAttack = true;
    private bool isAttacking = false;
    public Transform attackPoint;
    [SerializeField] GameObject attackCollision;
    
    [SerializeField] Transform[] patrolPoint;
    public int destinationPoint;
    [SerializeField] float verticalViewHeight = 3;

    public EnemyBehaviour Behaviour = EnemyBehaviour.STANDING;

    void Start()
    {
        playerPosition = GameObject.FindWithTag("Player").transform;
        attackDurationTimer = attackDuration; //valor únicament creat per després ser portat a un altre script
        canAttack = true;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float enemyToPlayerDistance = Vector2.Distance(transform.position, playerPosition.position); //float per veure la distancia en TOTES LES DIRECCIONS entre el jugador i l'enemic
        float verticalDetection = Mathf.Abs(playerPosition.position.y - transform.position.y); //float per crear un valor que és la diferència entre l'ALTURA del jugador i l'enemic

        switch (Behaviour)
        {
            case EnemyBehaviour.STANDING:

                if (isAttacking) return;

                if (enemyToPlayerDistance < attackRange && canAttack && verticalDetection < verticalViewHeight) //Prepara l'atac quan el jugador està suficientment aprop i en el seu rang de visió
                {
                    print("I'm going to attack");
                    isAttacking = true;
                    rb.velocity = Vector2.zero;
                    StartCoroutine(Attack());
                }
                else if (enemyToPlayerDistance <= detectionRange && verticalDetection < verticalViewHeight) //S'apropa al jugador per que estigui dins el rang d'atac i en el seu rang de visió
                {
                    print("im chasing");
                    isChasing();
                }
                else { rb.velocity = Vector2.zero; } //Com l'estat es STANDING, es queda esperant a algun canvi
                break;

            case EnemyBehaviour.PATROL:
                if (isAttacking) return;
                if (enemyToPlayerDistance < attackRange && canAttack && verticalDetection < verticalViewHeight) //Prepara l'atac quan el jugador està suficientment aprop i en el seu rang de visió
                {
                    print("I'm going to attack");
                    isAttacking = true;
                    rb.velocity = Vector2.zero;
                    StartCoroutine(Attack());
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
                        transform.localScale = new Vector3(1, 1, 1);
                        transform.position = Vector2.MoveTowards(transform.position, patrolPoint[0].position, moveSpeed * Time.deltaTime);
                        if (Vector2.Distance(transform.position, patrolPoint[0].position) < 0.1)
                        {
                            destinationPoint = 1;
                        }
                    }

                    if (destinationPoint == 1)
                    {
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
        Vector2 direction = playerPosition.position - transform.position; //Aqui fem un nou vector només per la direcció, els anteriors eren per saber la distancia o per saber l'altura relativa entre jugador i enemic.
        direction.y = 0; //fem que la direcció en y sigui 0
        direction = direction.normalized; //normalitzem el vector perque el valor sigui 1 o 0

        if (!isAttacking && canAttack)
        {
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y); //mou-te cap al jugador
            if (direction.x < 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    IEnumerator Attack()
    {
        //Temps de preparació de l'atack

        isAttacking = true;
        canAttack = false;
        if (playerPosition.position.x < transform.position.x) { transform.localScale = new Vector3(1, 1, 1); }
        else { transform.localScale = new Vector3(-1, 1, 1); }
        dashDirection = (playerPosition.position - transform.position);
        dashDirection.y = 0;
        dashDirection = dashDirection.normalized;
        attackTimer = 0f;

        yield return new WaitForSeconds(attackCooldown);


        //Fa el dash/atac cap a l'enemic
        attackHitbox();

        //Quan de temps dura el dash?
        while (attackTimer < attackDuration)
        {

            rb.velocity = dashDirection * dashSpeed;
            attackTimer += Time.deltaTime;
            yield return null;
        }

        //S'acaba el dash, aturat i posa't en cooldown
        rb.velocity = Vector2.zero;
        isAttacking = false;


        yield return new WaitForSeconds(attackCooldown);


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
}



