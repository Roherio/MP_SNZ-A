using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Aquest script nom�s serveix per activar al boss final un cop arribes al final del nivell
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
