using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//PROPI
//Script que fiquem dins el prefab de la colisio d'atac de l'enemic per tal que faci mal
public class AttackPointJabali : MonoBehaviour
{
    [SerializeField] float enemyAttackValue;
    [SerializeField] float showRadius;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, (JabaliEnemyScript.attackDurationTimer));
    }

    // Update is called once per frame
    void Update()
    {
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
            //print("L'atac ha fet hit per " + enemyAttackValue + " punts de mal! La vida actual de Liora �s " + GameControl_Script.lifeLiora);
            Destroy(gameObject);
            
        }
        else
        {
            Invoke("Destruir", 0.5f);
        }
    }
    public void Destruir()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmos() //ajuda visual per veure el radi de l'atac en l'inspector
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, showRadius);
    }
}