using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AguilaEnemyScript : MonoBehaviour
{
    public float angleOffset = 45f;
    private GameObject playerPosition;

    [SerializeField] GameObject projectile;
    [SerializeField] Transform projectilePosition;

    [SerializeField] float attackCooldown = 2f;
    private float attackTimer;
    public bool InSight;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackCooldown && InSight)
        {
            attackTimer = 0;
            featherAttack();
        }
    }
    private void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, playerPosition.transform.position - transform.position);
        if (ray.collider != null)
        {
            InSight = ray.collider.CompareTag("Player");
            if (InSight) { Debug.DrawRay(transform.position, playerPosition.transform.position - transform.position, Color.green); }
            else { Debug.DrawRay(transform.position, playerPosition.transform.position - transform.position, Color.red); }
        }
    }

    void featherAttack()
    {
        Vector3 directionToPlayer = playerPosition.transform.position - transform.position;
        float baseRotation = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        InstantiateProjectile(baseRotation);
    }

    void InstantiateProjectile(float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Instantiate(projectile, transform.position, rotation);
    }
}