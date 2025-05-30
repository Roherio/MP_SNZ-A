using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary_Script : MonoBehaviour
{
    public GameObject[] objetosInventario;
    public bool[] obtenidosObjetosInventario;
    private void Awake()
    {
        for (int i= 0; i < obtenidosObjetosInventario.Length; i++)
        {
            obtenidosObjetosInventario[i] = false;
        }
    }
}