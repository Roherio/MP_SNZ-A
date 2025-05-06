using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UrsinaScript : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    [SerializeField] float moveSpeed;

    private hpEnemiesScript hpEnemiesScript;

    //Control de estados
    private bool canAttack = true;
    private bool isAttacking = false;
    public float chaseTime;
    public float enemyScale = 1f;

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
    [SerializeField] GameObject clawIceSpikeCollision;
    [SerializeField] GameObject smashAttackCollision;
    [SerializeField] GameObject smashAttackCollisionPhase2;
    [SerializeField] Transform playerPosition;

    void Start()
    {
        hpEnemiesScript = GetComponent<hpEnemiesScript>();
        animator = GetComponent<Animator>();
        attackDurationTimer = clawAttackDuration; //valor únicament creat per després ser portat a un altre script
        playerPosition = GameObject.FindWithTag("Player").transform;
        canAttack = true;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //float per veure la distancia en TOTES LES DIRECCIONS entre el jugador i l'enemic
        float enemyToPlayerDistance = Vector2.Distance(transform.position, playerPosition.position); 

        //Controla que no fa cap altre acció mentre estigui atacant
        if (isAttacking) return; 

        //Entra en segona fase
        if (hpEnemiesScript.enemyHP <= hpEnemiesScript.maxEnemyHP / 2) { OnPhase2 = true; }

        //Si esta molt lluny el jugador
        if (enemyToPlayerDistance > farDistance && !isAttacking && canAttack)
        {
                Vector2 direction = playerPosition.position - transform.position; //Aqui fem un nou vector només per la direcció, els anteriors eren per saber la distancia o per saber l'altura relativa entre jugador i enemic.
                direction.y = 0; //fem que la direcció en y sigui 0
                direction = direction.normalized; //normalitzem el vector perque el valor sigui 1 o 0
                rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y); //mou-te cap al jugador
                if (direction.x < 0) { transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale); }
                else { transform.localScale = new Vector3(-enemyScale, enemyScale, enemyScale); }
        }
        //Si esta lluny del jugador pero suficientment a prop com per atacar
        if (enemyToPlayerDistance <= farDistance && enemyToPlayerDistance > mediumDistance && !isAttacking && canAttack) 
        {
            if(!OnPhase2)
            {
                int longRangeAttack = Random.Range(0, 3);
                print(longRangeAttack);
                if (longRangeAttack == 0f || longRangeAttack == 1f)
                {
                    print("I should chase");
                    isChasing();
                }
                if (longRangeAttack == 1f)
                {
                    print("I should Claw Attack");
                    StartCoroutine(clawAttackPhase2());
                }
                if (longRangeAttack == 2f)
                {
                    print("I should do a Smash Attack");
                    StartCoroutine(smashAttack());
                }
            }

            if (OnPhase2)
            {
                int longRangeAttack = Random.Range(0, 5);
                print(longRangeAttack);
                if (longRangeAttack == 0f || longRangeAttack == 1f)
                {
                    print("I should chase");
                    isChasing();
                }
                print(longRangeAttack);
                if (longRangeAttack == 2f || longRangeAttack == 3f)
                {
                    print("I should Claw Attack");
                    StartCoroutine(clawAttackPhase2());
                }
                if (longRangeAttack == 4f)
                {
                    print("I should do a Smash Attack");
                    StartCoroutine(smashAttack());
                }
            }
        }


        //Si esta a mitja distancia
        if (enemyToPlayerDistance <= mediumDistance && enemyToPlayerDistance > nearDistance && !isAttacking && canAttack)
        {
            if (!OnPhase2)
            {
                int mediumRangeAttack = Random.Range(0, 7);
                print(mediumRangeAttack);
                if (mediumRangeAttack == 0f || mediumRangeAttack == 1f || mediumRangeAttack == 2f)
                {
                    print("I should chase");
                    isChasing();
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
            if (OnPhase2)
            {
                int mediumRangeAttack = Random.Range(0, 7);
                print(mediumRangeAttack);
                if (mediumRangeAttack == 0f || mediumRangeAttack == 1f)
                {
                    print("I should chase");
                    isChasing();
                }
                if (mediumRangeAttack == 2f || mediumRangeAttack == 3f)
                {
                    print("I should Smash Attack");
                    StartCoroutine(smashAttack());
                }
                if (mediumRangeAttack == 4f || mediumRangeAttack == 5f)
                {
                    print("I should Claw Attack");
                    StartCoroutine(clawAttackPhase2());
                }
                if (mediumRangeAttack == 6f)
                {
                    print("I should do a Roar");
                    StartCoroutine(Roar());
                }
            }
        }        

        //Si esta molt aprop
        if (enemyToPlayerDistance <= nearDistance && !isAttacking && canAttack)
        {
            int closeRangeAttack = Random.Range(0, 11);
            print(closeRangeAttack);

            if (closeRangeAttack == 0f)
            {
                print("I should do a Roar");
                StartCoroutine(Roar());
            }
            if (closeRangeAttack == 1f || closeRangeAttack == 2f)
            {
                print("I should Smash Attack");
                StartCoroutine(smashAttack());

            }
            else
            {
                print("I should Claw Attack");
                StartCoroutine(clawAttack());
            }
        }

    }


    //Funció per perseguir al jugador
    void isChasing()
    {
        animator.SetBool("isWalking", true);
        float chaseTimer = 0f;
        isAttacking = true;
        canAttack = false;
        while (chaseTimer < chaseTime)
        {
            Vector2 direction = playerPosition.position - transform.position; //Aqui fem un nou vector només per la direcció, els anteriors eren per saber la distancia o per saber l'altura relativa entre jugador i enemic.
            direction.y = 0; //fem que la direcció en y sigui 0
            direction = direction.normalized; //normalitzem el vector perque el valor sigui 1 o 0

            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y); //mou-te cap al jugador
            if (direction.x < 0) { transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale); }
            else { transform.localScale = new Vector3(-enemyScale, enemyScale, enemyScale); }

            chaseTimer += Time.deltaTime;
        }
        rb.velocity = Vector3.zero;
        isAttacking = false;
        animator.SetBool("isWalking", false);
        canAttack = true;
    }
    /*IEnumerator isChasing()
    {
        float chaseTimer = 0f;
        isAttacking = true;
        canAttack = false;
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
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(chaseTime + 0.5f);
        isAttacking = false;
        canAttack = true;
    }*/


    IEnumerator clawAttack()
    {
        animator.SetBool("isClawAttacking", true);
        //Temps de preparació de l'atac
        isAttacking = true;
        canAttack = false;
        if (playerPosition.position.x < transform.position.x) { transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale); }
        else { transform.localScale = new Vector3(-enemyScale, enemyScale, enemyScale); }
        dashDirection = (playerPosition.position - transform.position);
        dashDirection.y = 0;
        dashDirection = dashDirection.normalized;
        attackTimer = 0f;

        yield return new WaitForSeconds(AttackCooldown);

        //Fa el dash/atac cap a l'enemic
        clawAttackInstance();
        

        //Quan de temps dura l'atac?
        while (attackTimer < clawAttackDuration)
        {

            rb.velocity = dashDirection * dashSpeed;
            attackTimer += Time.deltaTime;
            yield return null;
        }

        //S'acaba l'atac, aturat i posa't en cooldown
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(AttackCooldown);

        //Pot tornar a atacar
        animator.SetBool("isClawAttacking", false);
        isAttacking = false;
        if (playerPosition.position.x < transform.position.x) { transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale); }
        else { transform.localScale = new Vector3(-enemyScale, enemyScale, enemyScale); }
        canAttack = true;
    }

    IEnumerator clawAttackPhase2()
    {
        animator.SetBool("isClawAttacking", true);
        //Temps de preparació de l'atac
        isAttacking = true;
        canAttack = false;
        if (playerPosition.position.x < transform.position.x) { transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale); }
        else { transform.localScale = new Vector3(-enemyScale, enemyScale, enemyScale); }
        dashDirection = (playerPosition.position - transform.position);
        dashDirection.y = 0;
        dashDirection = dashDirection.normalized;
        attackTimer = 0f;

        yield return new WaitForSeconds(AttackCooldown);

        //Fa el dash/atac cap a l'enemic
        clawAttackInstance();
        clawIceSpikeInstance();


        //Quan de temps dura l'atac?
        while (attackTimer < clawAttackDuration)
        {

            rb.velocity = dashDirection * dashSpeed;
            attackTimer += Time.deltaTime;
            yield return null;
        }

        //S'acaba l'atac, aturat i posa't en cooldown
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(AttackCooldown);

        //Pot tornar a atacar
        animator.SetBool("isClawAttacking", false);
        isAttacking = false;
        if (playerPosition.position.x < transform.position.x) { transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale); }
        else { transform.localScale = new Vector3(-enemyScale, enemyScale, enemyScale); }
        canAttack = true;
    }
    IEnumerator smashAttack()
    {
        animator.SetBool("isHowling", true);
        //Temps de preparació de l'atac
        isAttacking = true;
        canAttack = false;
        if (playerPosition.position.x < transform.position.x) { transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale); }
        else { transform.localScale = new Vector3(-enemyScale, enemyScale, enemyScale); }
        attackTimer = 0f;

        yield return new WaitForSeconds(1.5f);
        animator.SetBool("isHowling", false);

        if (!OnPhase2) { smashAttackInstance(); }
        if (OnPhase2)
        {
            smashAttackInstancePhase2();
            yield return new WaitForSeconds(1f);
            smashAttackInstancePhase2();
            yield return new WaitForSeconds(1f);
            smashAttackInstancePhase2();
        }

        yield return new WaitForSeconds(3f);

        //Pot tornar a atacar
        
        isAttacking = false;
        if (playerPosition.position.x < transform.position.x) { transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale); }
        else { transform.localScale = new Vector3(-enemyScale, enemyScale, enemyScale); }
        canAttack = true;
    }

    IEnumerator Roar()
    {
        animator.SetBool("isHowling", true);
        CinemachineShake.Instance.ShakeCamera(10f, 1f);
        //Temps de preparació de l'atac
        isAttacking = true;
        canAttack = false;
        yield return new WaitForSeconds(AttackCooldown);
        roarInstance();

        while (attackTimer < clawAttackDuration)
        {

            rb.velocity = dashDirection * dashSpeed;
            attackTimer += Time.deltaTime;
            yield return null;
        }

        //S'acaba
        rb.velocity = Vector2.zero;
        


        yield return new WaitForSeconds(AttackCooldown);
        animator.SetBool("isHowling", false);

        //Pot tornar a atacar
        isAttacking = false;
        if (playerPosition.position.x < transform.position.x) { transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale); }
        else { transform.localScale = new Vector3(-enemyScale, enemyScale, enemyScale); }
        canAttack = true;
    }

    public void clawAttackInstance() //Genera la hitbox de l'atac només quan nosaltres volem
    {
        Instantiate(clawAttackCollision, attackPoint.transform, worldPositionStays: false);
    }

    public void clawIceSpikeInstance()
    {
        Instantiate(clawIceSpikeCollision, clawIceSpike_1_Spawn, worldPositionStays: false);
        Instantiate(clawIceSpikeCollision, clawIceSpike_2_Spawn, worldPositionStays: false);
        Instantiate(clawIceSpikeCollision, clawIceSpike_3_Spawn, worldPositionStays: false);
    }

    public void smashAttackInstance()
    {
        Instantiate(smashAttackCollision, smashAttackSpawn.transform, worldPositionStays: false);
    }

    public void roarInstance()
    {
        Instantiate(roarCollision,transform, worldPositionStays: false);
    }
    public void smashAttackInstancePhase2()
    {
        Instantiate(smashAttackCollisionPhase2);
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
