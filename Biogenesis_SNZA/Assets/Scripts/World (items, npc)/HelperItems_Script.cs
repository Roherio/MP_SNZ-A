using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperItems_Script
{
    //funci� que generar� un ID �nic per cada gameObject que es generi i li volguem donar un ID
    public static string GenerateUniqueID(GameObject gameObject)
    {
        return $"{gameObject.name}_{gameObject.transform.position.x}_{gameObject.transform.position.y}";
    }
}
