using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municion_Script : MonoBehaviour
{
    public float velocidad = 1f;
    public float variacioAltura = 0.5f;

    private Vector3 startPosition;
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
}
