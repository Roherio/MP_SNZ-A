using UnityEngine;


//PROPI
//Script que fiquem dins el prefab de la colisio d'atac del boss final per tal que faci mal
public class SmashAttackScript : MonoBehaviour
{

    [SerializeField] Transform playerPosition;
    [SerializeField] float enemyAttackValue;
    public Rigidbody2D rb;
    private Vector2 attackDirection;
    [SerializeField] float spreadSpeed;
    private Vector3 scaleChange;

    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(1.5f, 1.5f, 1.5f);
        rb = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindWithTag("Player").transform;
        Destroy(gameObject, 1.5f);
        print(GameControl_Script.lifeLiora);
        attackDirection = (playerPosition.position - transform.position);
        attackDirection.y = 0;
        attackDirection = attackDirection.normalized;
    }

    // Update is called once per frame
    void Update() //aquest també fa que es mogui en direccio al jugador, pero no el persegueix, sino que guarda la posicio a l'Start i es mou cap alla
    {
        rb.velocity = attackDirection.normalized * spreadSpeed;
        transform.localScale += scaleChange * Time.deltaTime;
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

            DamageLiora_Script.RecibirDamage(transform.position, enemyAttackValue);

            //
            Destroy(gameObject);
            //print("L'atac ha fet hit per " + enemyAttackValue + " punts de mal! La vida actual de Liora és " + GameControl_Script.lifeLiora);
        }

    }
}