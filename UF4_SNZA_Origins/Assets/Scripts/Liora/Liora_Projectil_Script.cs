using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liora_Projectil_Script : MonoBehaviour
{
    public Animator animator;
    public static bool facingRight = true;
    [SerializeField] private float speedProjectil = 20f;
    private float maxSpeed = 40f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speedProjectil/maxSpeed, 0, 0); 
    }
}
