using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municion_Script : MonoBehaviour
{
    //script propi, inspirant-nos en la funció Mathf.Sin que hem treballat a classe pel pulpi sinusoidal
    public float velocidad = 1f;
    public float variacioAltura = 0.5f;

    private Vector2 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(startPosition.x, startPosition.y + (Mathf.Sin(Time.time * velocidad) * variacioAltura));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //sumem munició al player al recollirla
            GameControl_Script.municion += 10;
            Destroy(gameObject);
        }
    }
}