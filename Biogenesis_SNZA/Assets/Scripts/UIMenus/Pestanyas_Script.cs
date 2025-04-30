using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pestanyas_Script : MonoBehaviour
{
    public Image[] pestanyasImages;
    public GameObject[] paginas;
    public int numeroPestanya;
    // Start is called before the first frame update
    void Start()
    {
        numeroPestanya = 0;
        ActivarPestanya(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            numeroPestanya--;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            numeroPestanya++;
        }
        if (numeroPestanya < 0)
        {
            numeroPestanya = 3;
        }
        if (numeroPestanya > 3)
        {
            numeroPestanya = 0;
        }
        ActivarPestanya(numeroPestanya);
    }

    public void ActivarPestanya(int tabNumber)
    {
        for (int i = 0; i < paginas.Length; i++)
        {
            paginas[i].SetActive(false);
            pestanyasImages[i].color = Color.grey;
        }
        paginas[tabNumber].SetActive(true);
        pestanyasImages[tabNumber].color = Color.white;
    }
}
