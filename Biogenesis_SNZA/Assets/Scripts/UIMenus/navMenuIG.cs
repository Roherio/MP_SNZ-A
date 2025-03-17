using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class navMenuIG : MonoBehaviour
{
    public GameObject MenuInGame;

    public GameObject[] ArrTxtMenuIG = new GameObject[6];
    public Button[] ArrBotones = new Button[6];
    public static int idBotonActivo = 0;
    public Vector3 botonGrande = new Vector3(1.1f, 1.1f, 1);
    private Vector3 botonNormal = new Vector3(1, 1, 1);

    public static int life = 100, money = 100, adrenaline = 100;    //Aqui todos los ints
    public static int[] intsActuales = {life, money, adrenaline};   // AQUI LOS QUE SE TENGAN QUE GUARDAR

    // Start is called before the first frame update
    void Start()
    {
        MenuInGame.SetActive(false);

        // *****BOTONES*****
        for (int i = 0; i < ArrTxtMenuIG.Length; i++)   // Desactivar todos los textos de todos los subapartados
        {
            ArrTxtMenuIG[i].SetActive(false);
        }
        idBotonActivo = scrMemoria.memIdBotonActivo;    // Activa solo el último boton que se pulsó
        BotonPulsado(idBotonActivo);

        // *****TEST MEMORIA*****
        for (int i = 0; i < scrMemoria.intsMemoria.Length; i++)   // CARGAR LAS VARIABLES GUARDADAS
        {
            intsActuales[i] = scrMemoria.intsMemoria[i];
            Debug.Log(intsActuales[i]);
        }

        life++;
        money += 5;
        adrenaline += 100;
        Sincronizar();

        for (int i = 0; i < intsActuales.Length; i++)   //GUARDAR LAS VARIABLES ACTUALES
        {
            scrMemoria.intsMemoria[i] = intsActuales[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Sincronizar()  // Para no cambiar manualmente el valor de cada indice de la array (seria incomodo), cambiar directamente el valor de la variable y que haya esta función que sincronize todas con la array
    {
        intsActuales[0] = life;
        intsActuales[1] = money;
        intsActuales[2] = adrenaline;
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
    public void CMenuG()
    {
        MenuInGame.SetActive(!MenuInGame.activeSelf);
        if (MenuInGame.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}