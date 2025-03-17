using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class navMenuIG : MonoBehaviour
{

    public GameObject[] ArrTxtMenuIG = new GameObject[6];
    public Button[] ArrBotones = new Button[6];
    public static int idBotonActivo = 0;
    public Vector3 botonGrande = new Vector3(1.1f, 1.1f, 1);
    private Vector3 botonNormal = new Vector3(1, 1, 1);
    public static int life = 100;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ArrTxtMenuIG.Length; i++)
        {
            ArrTxtMenuIG[i].SetActive(false);
        }
        idBotonActivo = scrMemoria.memIdBotonActivo;
        ArrTxtMenuIG[idBotonActivo].SetActive(true);
        BotonPulsado(idBotonActivo);
        life++;
        scrMemoria.ActualizarValores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetBotones()
    {
        
    }
    public void BotonPulsado(int idBotonPulsado)
    {
        ArrTxtMenuIG[idBotonActivo].SetActive(false);
        ArrBotones[idBotonActivo].transform.localScale = botonNormal;
        idBotonActivo = idBotonPulsado;
        scrMemoria.GuardarIdBotonActivo();
        ArrTxtMenuIG[idBotonActivo].SetActive(true);
        ArrBotones[idBotonActivo].transform.localScale = botonGrande;
    }
}