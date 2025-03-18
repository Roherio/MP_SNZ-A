using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class navMenuIG : MonoBehaviour
{
    public GameObject MenuInGame;
    //*****BOTONES Y UI*****
    public GameObject[] ArrSubmenus = new GameObject[6];
    public Button[] ArrBSubmenus = new Button[6];
    public GameObject[] ArrMenusEnemigos = new GameObject[4];
    public Button[] ArrBTutoriales = new Button[4];
    public Button[] ArrBObjetos = new Button[4];
    public Button[] ArrBPulsera = new Button[4];
    public Button[] ArrBEnemigos = new Button[4];
    public Button[] ArrBNotas = new Button[4];
    public Button[] ArrBOpciones = new Button[4];

    public Button[][] ArrBotones = new Button[6][];

    public static int idBotonActivo = 0;
    public static int idSubBotonActivo = 0;

    private Vector3 botonGrande = new Vector3(1.1f, 1.1f, 1);
    private Vector3 botonNormal = new Vector3(1, 1, 1);
    // *****VARIABLES*****
    public static int life = 100, money = 100, adrenaline = 100;    // Aqui todos los ints (ESTOS NO ACTUALIZAN LOS DE LA ARRAY, para eso la función de SINCRONIZAR)
    public static int[] intsActuales = {life, money, adrenaline};   // AQUI LOS QUE SE TENGAN QUE GUARDAR

    // Start is called before the first frame update
    void Start()
    {
        ArrBotones[0] = ArrBTutoriales;
        ArrBotones[1] = ArrBObjetos;
        ArrBotones[2] = ArrBPulsera;
        ArrBotones[3] = ArrBEnemigos;
        ArrBotones[4] = ArrBNotas;
        ArrBotones[5] = ArrBOpciones;

        MenuInGame.SetActive(false);
        AsignarListeners();

        // *****BOTONES*****
        DesactivarSubmenus();
        idBotonActivo = scrMemoria.memIdBotonActivo;    // esto podria ir dentro de la array tambien
        ArrBSubmenus[idBotonActivo].onClick.Invoke();   // Activa solo el último boton que se pulsó
        ArrBotones[idBotonActivo][idSubBotonActivo].onClick.Invoke();

        // *****TEST MEMORIA*****
        CargarVariables();

        life++;
        money += 5;
        adrenaline += 100;
        Sincronizar();

        GuardarVariables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AsignarListeners()
    {
        // Asignar listeners para los botones de submenú
        for (int i = 0; i < ArrBSubmenus.Length; i++)
        {
            int boton = i;  // Capturamos el índice del botón
            ArrBSubmenus[i].onClick.AddListener(() => BotonPulsado(boton, 0));  // 0 indica que es un botón de submenú
        }

        // Asignar listeners para los botones de enemigos
        for (int i = 0; i < ArrBEnemigos.Length; i++)
        {
            int boton = i;  // Capturamos el índice del botón
            ArrBEnemigos[i].onClick.AddListener(() => BotonPulsado(boton, 1));  // 1 indica que es un botón de enemigos
        }
    }
    void DesactivarSubmenus()
    {
        for (int i = 0; i < ArrSubmenus.Length; i++)   // Desactivar todos los textos de todos los subapartados (hacer funcion)
        {
            ArrSubmenus[i].SetActive(false);
        }
    }
    void DesactivarMEnemigos()
    {
        for (int i = 0; i < ArrMenusEnemigos.Length; i++)   // Desactivar todos los textos de todos los subapartados (hacer funcion)
        {
            ArrMenusEnemigos[i].SetActive(false);
        }

    }
    void CargarVariables()
    {
        for (int i = 0; i < scrMemoria.intsMemoria.Length; i++)   // CARGAR LAS VARIABLES GUARDADAS
        {
            intsActuales[i] = scrMemoria.intsMemoria[i];
            Debug.Log(intsActuales[i]);
        }
    }
    void GuardarVariables()
    {
        for (int i = 0; i < intsActuales.Length; i++)   //GUARDAR LAS VARIABLES ACTUALES
        {
            scrMemoria.intsMemoria[i] = intsActuales[i];
        }
    }
    void Sincronizar()  // Actualizar la array con los nuevos valores de cada variable por separado (de normal, al cambiar el valor de una variable, no se cambia dentro de la array)
                        // todo esto igual no hace falta si hay funciones de "SumarVida" y tal, aunque depende de como se les haga mas comodo a ellos, si referenciar el valor dentro de la array directamente (que como es hacerlo una vez y ya tampoco es tan incomodo) o de esta manera.
    {
        intsActuales[0] = life;
        intsActuales[1] = money;
        intsActuales[2] = adrenaline;
    }
    public void BotonPulsado(int idBotonPulsado, int tipoBoton)
    {
        switch (tipoBoton)
        {
            case 0:
                ManejarBoton(idBotonPulsado, ArrSubmenus, ArrBSubmenus, false);
                break;
            case 1:
                ManejarBoton(idBotonPulsado, ArrMenusEnemigos, ArrBEnemigos, true);
                break;
        }
    }

    /*public void BotonPulsado(int idBotonPulsado)
    {
        ArrSubmenus[idBotonActivo].SetActive(false);
        ArrBSubmenus[idBotonActivo].transform.localScale = botonNormal;
        idBotonActivo = idBotonPulsado;
        scrMemoria.GuardarIdBotonActivo();
        ArrSubmenus[idBotonActivo].SetActive(true);
        ArrBSubmenus[idBotonActivo].transform.localScale = botonGrande;
    }*/

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

    // Función genérica para manejar tanto submenús como enemigos
    private void ManejarBoton(int idBotonPulsado, GameObject[] arrayElementos, Button[] arrayBotones, bool subMenu)
    {
        if (subMenu == false)
        {
            arrayElementos[idBotonActivo].SetActive(false);
            //arrayBotones[idBotonActivo].transform.localScale = botonNormal;

            idBotonActivo = idBotonPulsado;

            scrMemoria.GuardarIdBotonActivo();

            arrayElementos[idBotonActivo].SetActive(true);
            ArrBotones[idBotonActivo][idSubBotonActivo].onClick.Invoke();
            //arrayBotones[idBotonActivo].transform.localScale = botonGrande;
        }
        if (subMenu == true)
        {
            arrayElementos[idSubBotonActivo].SetActive(false);
            DesactivarMEnemigos();
            //arrayBotones[idBotonActivo].transform.localScale = botonNormal;

            idSubBotonActivo = idBotonPulsado;

            scrMemoria.GuardarIdBotonActivo();

            arrayElementos[idSubBotonActivo].SetActive(true);
            //arrayBotones[idBotonActivo].transform.localScale = botonGrande;
        }
    }
}
