using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class navMenuIG : MonoBehaviour
{
    public GameObject MenuInGame;
    //*****BOTONES Y UI*****
    
    public Button[] ArrBMenus = new Button[6];
    public GameObject[] ArrMenus = new GameObject[6];
    public Button[] ArrBTutoriales = new Button[4];
    public GameObject[] ArrMenusTutoriales = new GameObject[4];
    public Button[] ArrBObjetos = new Button[4];
    public GameObject[] ArrMenusObjetos = new GameObject[4];
    public Button[] ArrBPulsera = new Button[4];
    public GameObject[] ArrMenusPulsera = new GameObject[4];
    public Button[] ArrBEnemigos = new Button[4];
    public GameObject[] ArrMenusEnemigos = new GameObject[4];
    public Button[] ArrBNotas = new Button[4];
    public GameObject[] ArrMenusNotas = new GameObject[4];
    public Button[] ArrBOpciones = new Button[3];
    public GameObject[] ArrMenusOpciones = new GameObject[3];

    public Button[][] ArrBotones = new Button[6][];
    public GameObject[][] ArrSubmenus = new GameObject[6][];

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

        ArrSubmenus[0] = ArrMenusTutoriales;
        ArrSubmenus[1] = ArrMenusObjetos;
        ArrSubmenus[2] = ArrMenusPulsera;
        ArrSubmenus[3] = ArrMenusEnemigos;
        ArrSubmenus[4] = ArrMenusNotas;
        ArrSubmenus[5] = ArrMenusOpciones;

        AsignarListeners();
        MenuInGame.SetActive(false);
        

        // *****BOTONES*****
        DesactivarMenus();
        idBotonActivo = scrMemoria.memIdBotonActivo;    // esto podria ir dentro de la array tambien
        ArrBMenus[idBotonActivo].onClick.Invoke();   // Activa solo el último boton que se pulsó
        

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
        // Asignar listeners para los botones del menu
        for (int i = 0; i < ArrBMenus.Length; i++)
        {
            int boton = i;
            ArrBMenus[i].onClick.AddListener(() => BotonPulsado(boton, 6));
        }

        // Asignar listeners para los botones de los submenus
        for (int i = 0; i < ArrBEnemigos.Length; i++)
        {
            int boton = i;  
            ArrBEnemigos[i].onClick.AddListener(() => BotonPulsado(boton, 3));
        }
        for (int i = 0; i < ArrBOpciones.Length; i++)
        {
            int boton = i;
            ArrBOpciones[i].onClick.AddListener(() => BotonPulsado(boton, 5));
        }


        /*
         *    ********                                                           ********
         * ******** RESTOS/RELIQUIA DE LA MAYOR PERDIDA DE TIEMPO NUNCA ANTES VISTA ********
         *    ********                                                           ********
         * 
         * 
        for (int idMenu = 0; idMenu < ArrBotones.Length; idMenu++)
        {
            for (int j = 0; j < ArrBotones[idMenu].Length; j++)
            {
                if (ArrBotones[idMenu][j] != null)
                {
                    int botonIndex = j;
                    ArrBotones[idMenu][j].onClick.AddListener(() => BotonPulsado(botonIndex, idMenu));
                }
                else
                {
                    
                }
            }
        }
        */
    }

    void DesactivarMenus()
    {
        for (int i = 0; i < ArrMenus.Length; i++)   // Desactivar todos los textos de todos los subapartados (hacer funcion)
        {
            ArrMenus[i].SetActive(false);
        }
    }

    void DesactivarSubmenus()
    {
        foreach (GameObject[] menu in ArrSubmenus)
        {
            foreach (GameObject submenu in menu)
            {
                if (submenu != null)
                {
                    submenu.SetActive(false);
                }
            }
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
    {
        intsActuales[0] = life;
        intsActuales[1] = money;
        intsActuales[2] = adrenaline;
    }

    public void BotonPulsado(int idBotonPulsado, int tipoBoton)
    {
        switch (tipoBoton)
        {

            case 6:
                    ManejarBoton(idBotonPulsado, ArrMenus, ArrBMenus, false);
                break;
            case 3:
                    ManejarBoton(idBotonPulsado, ArrMenusEnemigos, ArrBEnemigos, true);
                break;
            case 5:
                    ManejarBoton(idBotonPulsado, ArrMenusOpciones, ArrBOpciones, true);
                break;
            case 4:
                    ManejarBoton(idBotonPulsado, ArrMenusNotas, ArrBNotas, true);
                break;
            case 2:
                    ManejarBoton(idBotonPulsado, ArrMenusPulsera, ArrBPulsera, true);
                break;
            case 1:
                    ManejarBoton(idBotonPulsado, ArrMenusObjetos, ArrBObjetos, true);
                break;
            case 0:
                    ManejarBoton(idBotonPulsado, ArrMenusTutoriales, ArrBTutoriales, true);
                break;
        }
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

    // Función genérica para manejar tanto submenús como enemigos
    private void ManejarBoton(int idBotonPulsado, GameObject[] arrayElementos, Button[] arrayBotones, bool subMenu)
    {
        if (subMenu == false)
        {
            arrayElementos[idBotonActivo].SetActive(false);
            DesactivarMenus();
            //arrayBotones[idBotonActivo].transform.localScale = botonNormal;

            idBotonActivo = idBotonPulsado;

            scrMemoria.GuardarIdBotonActivo();

            arrayElementos[idBotonActivo].SetActive(true);
            idSubBotonActivo = 0;
            ArrBotones[idBotonActivo][idSubBotonActivo].onClick.Invoke();
            //arrayBotones[idBotonActivo].transform.localScale = botonGrande;
        }
        if (subMenu == true)
        {
            arrayElementos[idSubBotonActivo].SetActive(false);
            DesactivarSubmenus();
            //arrayBotones[idBotonActivo].transform.localScale = botonNormal;

            idSubBotonActivo = idBotonPulsado;

            scrMemoria.GuardarIdBotonActivo();

            arrayElementos[idSubBotonActivo].SetActive(true);
            //arrayBotones[idBotonActivo].transform.localScale = botonGrande;
        }
    }
}
