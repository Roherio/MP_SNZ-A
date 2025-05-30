using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liora_Projectil_Script : MonoBehaviour
{
    public static Liora_Projectil_Script instance;
    public Animator animator;
    private bool facingRight = true;
    [SerializeField] private float speedProjectil = 20f;
    private float maxSpeed = 40f;
    private void Start()
    {
        Invoke("Destruir", 3f);
    }
    // Update is called once per frame
    void Update()
    {
        //variable que determinarà (-1 o 1) la direcció del projectil
        float direccio;
        if (facingRight) { direccio = 1f; transform.localScale = new Vector2(1f, 1f); } else { direccio = -1f; transform.localScale = new Vector2(-1f, 1f); }
        //utilitzem el  valor float direcció per dirli al projectil en quina direcció ha d'anar al instanciar-se.
        transform.Translate((speedProjectil / maxSpeed) * direccio, 0, 0);
    }
    public void SetDirection (bool direction)
    {
        facingRight = direction;
    }
    public void Destruir()
    {
        Destroy(gameObject);
    }
}
