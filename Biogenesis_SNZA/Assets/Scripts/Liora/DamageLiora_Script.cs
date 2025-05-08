using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLiora_Script : MonoBehaviour
{
    public static bool isParrying = false;
    public static Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            RecibirDamage(transform.position, 10);
        }
    }
    public static void RecibirDamage(Vector2 enemy, float damage)
    {
        if (isParrying)
        {
            print("he hecho parry suuui");
            return;
        }
        else if (!isParrying)
        {
            //Knockback effect
            knockbackScript kb = collider.GetComponent<knockbackScript>();
            if (kb != null)
            {
                kb.ApplyKnockback(enemy, damage);
            }
            GameControl_Script.lifeLiora -= damage;
        }
    }
}
