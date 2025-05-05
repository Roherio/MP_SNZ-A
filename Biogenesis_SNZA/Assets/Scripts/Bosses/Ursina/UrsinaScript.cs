using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UrsinaScript : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    [SerializeField] float moveSpeed;

    //Control de estados
    private bool canAttack = true;
    private bool isAttacking = false;
    public float chaseTime;

    //Spawn de instancias
    public Transform attackPoint;
    public Transform smashAttackSpawn;
    public Transform clawIceSpike_1_Spawn;
    public Transform clawIceSpike_2_Spawn;
    public Transform clawIceSpike_3_Spawn;



    //Variables relacionadas con ataque
    private float attackTimer = 0f;
    public static float attackDurationTimer;
    [SerializeField] float clawAttackDuration;
    [SerializeField] float AttackCooldown;
    private Vector2 dashDirection;
    [SerializeField] float dashSpeed;

    //Distancias de detecccion
    [SerializeField] float farDistance;
    [SerializeField] float mediumDistance;
    [SerializeField] float nearDistance;


    public bool OnPhase2 = false;
    [SerializeField] GameObject roarCollision;
    [SerializeField] GameObject clawAttackCollision;
    [SerializeField] GameObject smashAttackCollision;
    [SerializeField] Transform playerPosition;

    void Start()
    {
        attackDurationTimer = clawAttackDuration; //valor únicament creat per després ser portat a un altre script
        playerPosition = GameObject.FindWithTag("Player").transform;
        canAttack = true;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        float enemyToPlayerDistance = Vector2.Distance(transform.position, playerPosition.position); //float per veure la distancia en TOTES LES DIRECCIONS entre el jugador i l'enemic
        if (isAttacking) return; //Controla que no fa cap altre acció mentre estigui atacant

            if (enemyToPlayerDistance > farDistance && !isAttacking && canAttack) //Si esta molt lluny el jugador
            {
                Vector2 direction = playerPosition.position - transform.position; //Aqui fem un nou vector només per la direcció, els anteriors eren per saber la distancia o per saber l'altura relativa entre jugador i enemic.
                direction.y = 0; //fem que la direcció en y sigui 0
                direction = direction.normalized; //normalitzem el vector perque el valor sigui 1 o 0
                rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y); //mou-te cap al jugador
                if (direction.x < 0) { transform.localScale = new Vector3(2, 2, 2); }
                else { transform.localScale = new Vector3(-2, 2, 2); }
            }
            if (enemyToPlayerDistance <= farDistance && enemyToPlayerDistance > mediumDistance && !isAttacking && canAttack) //Si esta lluny del jugador pero suficientment a prop com per atacar
            {
                int longRangeAttack = Random.Range(0, 3);
                print(longRangeAttack);


                if (longRangeAttack == 0f || longRangeAttack == 1f)
                {
                    print("I should chase");
                    StartCoroutine(isChasing());
                }
                if (longRangeAttack == 1f)
                {
                    print("I should do a Smash Attack");
                    StartCoroutine(smashAttack());
                }

            }
            if (enemyToPlayerDistance <= mediumDistance && enemyToPlayerDistance > nearDistance && !isAttacking && canAttack)
            {
                int mediumRangeAttack = Random.Range(0, 7);
                print(mediumRangeAttack);

                if (mediumRangeAttack == 0f || mediumRangeAttack == 1f || mediumRangeAttack == 2f)
                {
                     print("I should chase");
                     StartCoroutine(isChasing());
                }
                if (mediumRangeAttack == 3f || mediumRangeAttack == 4f || mediumRangeAttack == 5f)
                {
                    print("I should Smash Attack");
                    StartCoroutine(smashAttack());
                }
                if (mediumRangeAttack == 6f)
                {
                     print("I should do a Roar");
                     StartCoroutine(Roar());
                }
                    
            }
            if (enemyToPlayerDistance <= nearDistance && !isAttacking && canAttack) //Si esta molt lluny el jugador
            {
                int closeRangeAttack = Random.Range(0, 6);
                print(closeRangeAttack);

                if (closeRangeAttack == 0f || closeRangeAttack == 1f || closeRangeAttack == 2f)
                {
                    print("I should Claw Attack");
                    StartCoroutine(clawAttack());
                }
                if (closeRangeAttack == 3f || closeRangeAttack == 4f)
                {
                    print("I should do a Roar");
                    StartCoroutine(Roar());
                }
                if (closeRangeAttack == 5f)
                {
                    print("I should Smash Attack");
                    StartCoroutine(smashAttack());
                    
                }
                
            }

    }
     
    IEnumerator isChasing()
    {
        float chaseTimer = 0f;
        isAttacking = true;
        canAttack = false;

        yield return new WaitForSeconds(0.5f);

        while (chaseTimer < chaseTime)
        {
            Vector2 direction = playerPosition.position - transform.position; //Aqui fem un nou vector només per la direcció, els anteriors eren per saber la distancia o per saber l'altura relativa entre jugador i enemic.
            direction.y = 0; //fem que la direcció en y sigui 0
            direction = direction.normalized; //normalitzem el vector perque el valor sigui 1 o 0
         
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y); //mou-te cap al jugador
            if (direction.x < 0) { transform.localScale = new Vector3(2, 2, 2); }
            else { transform.localScale = new Vector3(-2, 2, 2); }

            chaseTimer += Time.deltaTime;
            yield return null;
        }

        isAttacking = false;
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }
    IEnumerator clawAttack()
    {
        //Temps de preparació de l'atack

        isAttacking = true;
        canAttack = false;
        if (playerPosition.position.x < transform.position.x) { transform.localScale = new Vector3(2, 2, 2); }
        else { transform.localScale = new Vector3(-2, 2, 2); }
        dashDirection = (playerPosition.position - transform.position);
        dashDirection.y = 0;
        dashDirection = dashDirection.normalized;
        attackTimer = 0f;

        yield return new WaitForSeconds(AttackCooldown);


        //Fa el dash/atac cap a l'enemic
        clawAttackInstance();
        if (OnPhase2)
        {

        }

        //Quan de temps dura el dash?
        while (attackTimer < clawAttackDuration)
        {

            rb.velocity = dashDirection * dashSpeed;
            attackTimer += Time.deltaTime;
            yield return null;
        }

        //S'acaba el dash, aturat i posa't en cooldown
        rb.velocity = Vector2.zero;
        isAttacking = false;


        yield return new WaitForSeconds(AttackCooldown);


        //Pot tornar a atacar
        if (playerPosition.position.x < transform.position.x) { transform.localScale = new Vector3(2, 2, 2); }
        else { transform.localScale = new Vector3(-2, 2, 2); }
        canAttack = true;
    }
    IEnumerator smashAttack()
    {
        //Temps de preparació de l'atack

        isAttacking = true;
        canAttack = false;
        if (playerPosition.position.x < transform.position.x) { transform.localScale = new Vector3(2, 2, 2); }
        else { transform.localScale = new Vector3(-2, 2, 2); }
        attackTimer = 0f;

        yield return new WaitForSeconds(AttackCooldown);

        smashAttackInstance();

        isAttacking = false;


        yield return new WaitForSeconds(AttackCooldown);


        //Pot tornar a atacar
        if (playerPosition.position.x < transform.position.x) { transform.localScale = new Vector3(2, 2, 2); }
        else { transform.localScale = new Vector3(-2, 2, 2); }
        canAttack = true;
    }

    IEnumerator Roar()
    {
        //Temps de preparació de l'atack

        isAttacking = true;
        canAttack = false;
        yield return new WaitForSeconds(AttackCooldown);


        //Fa el dash/atac cap a l'enemic
        roarInstance();

        //Quan de temps dura el dash?
        while (attackTimer < clawAttackDuration)
        {

            rb.velocity = dashDirection * dashSpeed;
            attackTimer += Time.deltaTime;
            yield return null;
        }

        //S'acaba el dash, aturat i posa't en cooldown
        rb.velocity = Vector2.zero;
        isAttacking = false;


        yield return new WaitForSeconds(AttackCooldown);


        //Pot tornar a atacar
        if (playerPosition.position.x < transform.position.x) { transform.localScale = new Vector3(2, 2, 2); }
        else { transform.localScale = new Vector3(-2, 2, 2); }
        canAttack = true;
    }

    public void clawAttackInstance() //Genera la hitbox de l'atac només quan nosaltres volem
    {
        Instantiate(clawAttackCollision, attackPoint.transform, worldPositionStays: false);
    }

    public void smashAttackInstance()
    {
        Instantiate(smashAttackCollision, smashAttackSpawn.transform, worldPositionStays: false);
    }

    public void roarInstance()
    {
        Instantiate(roarCollision,transform, worldPositionStays: false);
    }    
   

    private void OnDrawGizmos() //Ajudes visuals
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, nearDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, mediumDistance);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, farDistance);
    }
}
