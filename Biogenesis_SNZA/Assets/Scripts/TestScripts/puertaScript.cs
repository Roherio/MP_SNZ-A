using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puertaScript : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] GameObject player;
    [SerializeField] GameObject InteractButton;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("On TriggerBox");
        if (Input.GetKey(KeyCode.E))
        {
            print("Pressing E");
            Teleport();
        }
           
    }
    public void Teleport()
    {
        player.transform.position = destination.position;   
    }
}
   
