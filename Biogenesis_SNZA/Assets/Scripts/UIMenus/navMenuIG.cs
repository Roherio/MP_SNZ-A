using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navMenuIG : MonoBehaviour
{

    public GameObject[] ArrTxtMenuIG = new GameObject[6];
    public static int idBotonActivo = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ArrTxtMenuIG.Length; i++)
        {
            ArrTxtMenuIG[i].SetActive(false);
        }
        idBotonActivo = scrMemoria.memIdBotonActivo;
        ArrTxtMenuIG[idBotonActivo].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BotonPulsado(int idBotonPulsado)
    {
        ArrTxtMenuIG[idBotonActivo].SetActive(false);
        idBotonActivo = idBotonPulsado;
        scrMemoria.GuardarIdBotonActivo();
        ArrTxtMenuIG[idBotonActivo].SetActive(true);
    }
}