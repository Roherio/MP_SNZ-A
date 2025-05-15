using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventarioController_Script : MonoBehaviour
{
    public GameObject inventarioPanel;

    //////logica item en menú
    public GameObject[] objeto;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < objeto.Length; i++)
        {
            objeto[i].SetActive(false);
        }
    }
    public void ActivarItem(int itemNumber)
    {
        for (int i = 0; i < objeto.Length; i++)
        {
            objeto[i].SetActive(false);
        }
        objeto[itemNumber].SetActive(true);
    }
}