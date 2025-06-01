using UnityEngine;

//Aquest atac fa que sota el jugador vagin apareixent diferents blocs de gel que amb el temps exploten i fan mal
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
        GetComponent<BoxCollider2D>().enabled = false; //Com volem que facin mal quan exploten, al principi no volem la colisio
        playerPosition = GameObject.FindWithTag("Player").transform;
        enemyLocation = GameObject.FindWithTag("Ursina").transform;

    }
    void Start()
    {

        transform.position = new Vector2(playerPosition.position.x, enemyLocation.position.y); 

        scaleChange = new Vector3(1.5f, 1.5f, 1.5f);
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 1.5f);
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

            //Treu vida
            DamageLiora_Script.RecibirDamage(transform.position, enemyAttackValue);

            //
            Destroy(gameObject);
            //print("L'atac ha fet hit per " + enemyAttackValue + " punts de mal! La vida actual de Liora és " + GameControl_Script.lifeLiora);
        }

    }
}
