using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PROPI
//Aquest script només serveix per activar al boss final un cop arribes al final del nivell (o si tornes a arribar si et mata, ja que es desactiva quan tu mors)
public class ActivateUrsina : MonoBehaviour
{
    public GameObject Ursina;
    public GameObject UrsinaHealthBar;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) 
        { 
            Ursina.SetActive(true);
            Invoke("ursinaHealth", 1.5f);
        }
       
    }
    void ursinaHealth()
    {
        UrsinaHealthBar.SetActive(true);
    }

}
