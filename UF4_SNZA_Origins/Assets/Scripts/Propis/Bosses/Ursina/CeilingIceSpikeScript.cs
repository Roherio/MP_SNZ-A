using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PROPI
//Script que fiquem dins el prefab de la colisio d'atac del boss final per tal que faci mal
public class CeilingIceSpikeScript : MonoBehaviour
{
    [SerializeField] float enemyAttackValue;
    void Start()
    {
        Destroy(gameObject, 3f);
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