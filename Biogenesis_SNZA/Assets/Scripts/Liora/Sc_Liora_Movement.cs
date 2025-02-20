using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_Liora_Movement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    [SerializeField]float speed = 5f;
    Vector2 movim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(movim.x * speed, rb.velocity.y);
    }

    public void Movimiento(InputAction.CallbackContext context)
    {
        print("moviendosee");
        movim = context.ReadValue<Vector2>();
    }
}
