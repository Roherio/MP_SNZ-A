using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryCollider_Script : MonoBehaviour
{
    public float showRadius;
    public static float parryTime;
    public static float duracioCollider;
    private void Awake()
    {
        //DamageLiora_Script.isParrying = true;
    }
    private void Start()
    {
        Invoke("Destruir", duracioCollider);
    }
    private void Update()
    {
        DamageLiora_Script.isParrying = true;
    }
    private void Destruir()
    {
        DamageLiora_Script.isParrying = false;
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, showRadius);
    }
}
