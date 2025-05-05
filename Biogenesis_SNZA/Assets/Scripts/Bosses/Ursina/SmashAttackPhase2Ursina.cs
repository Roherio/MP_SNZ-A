using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashAttackPhase2Ursina: MonoBehaviour
{
    [SerializeField] float enemyAttackValue;
    private Transform playerPosition;
    public Transform enemyLocation;
    public Rigidbody2D rb;
  
    private Vector3 scaleChange;

    // Start is called before the first frame update
    private void Awake()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        playerPosition = GameObject.FindWithTag("Player").transform;
        enemyLocation = GameObject.FindWithTag("Ursina").transform;

    }
    void Start()
    {

        transform.position = new Vector2(playerPosition.position.x, enemyLocation.position.y);
        scaleChange = new Vector3(1.5f, 1.5f, 1.5f);
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f);
        print(GameControl_Script.lifeLiora);
        Invoke("enableHitBox", 1f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += scaleChange * Time.deltaTime;
    }

    void enableHitBox()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Consola
            print("player hit");

            //Feeling
            FindObjectOfType<HitStop>().hitStop(0.01f);
            CinemachineShake.Instance.ShakeCamera(10f, .01f);

            //Knockback effect
            knockbackScript kb = collision.GetComponent<knockbackScript>();
            if (kb != null)
            {
                kb.ApplyKnockback(transform.position, enemyAttackValue);
            }

            //Treu vida
            GameControl_Script.lifeLiora -= enemyAttackValue;

            //
            Destroy(gameObject);
            print("L'atac ha fet hit per " + enemyAttackValue + " punts de mal! La vida actual de Liora és " + GameControl_Script.lifeLiora);
        }

    }
}
