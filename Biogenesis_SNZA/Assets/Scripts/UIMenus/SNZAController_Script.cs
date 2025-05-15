using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNZAController_Script : MonoBehaviour
{
    public GameObject snzaPanel;

    //////logica item en menú
    public GameObject[] cristal;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < cristal.Length; i++)
        {
            cristal[i].SetActive(false);
        }
    }
    public void MostrarSNZA(int snzaNumber)
    {
        for (int i = 0; i < cristal.Length; i++)
        {
            cristal[i].SetActive(false);
        }
        cristal[snzaNumber].SetActive(true);
    }
}
