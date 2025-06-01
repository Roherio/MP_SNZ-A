using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider_Script : MonoBehaviour
{
    //showradius només serveix per l'inspector veure quan s'instancia l'atac i si ho fa correctament
    public float showRadius;
    public static float attackTime;
    //aquesta variable no cal que tingui valor perquè li donarem el valor de QUANT dura cada collider de attack instanciat en el moment d'instanciarlo (Liora_Attack_Script, línia 174)
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
