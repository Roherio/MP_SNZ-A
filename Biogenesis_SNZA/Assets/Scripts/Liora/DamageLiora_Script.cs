using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLiora_Script : MonoBehaviour
{
    public static bool isParrying = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            RecibirDamage(10);
        }
    }
    public static void RecibirDamage(float damage)
    {
        if (isParrying)
        {
            print("he hecho parry suuui");
        }
        else if (!isParrying)
        {
            GameControl_Script.lifeLiora -= damage;
        }
    }
}
