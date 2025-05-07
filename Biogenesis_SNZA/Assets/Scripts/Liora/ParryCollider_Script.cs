using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryCollider_Script : MonoBehaviour
{
    public float showRadius;
    public static float parryTime;
    public static float duracioCollider;
    private void Start()
    {
        Destroy(gameObject, duracioCollider);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, showRadius);
    }
}
