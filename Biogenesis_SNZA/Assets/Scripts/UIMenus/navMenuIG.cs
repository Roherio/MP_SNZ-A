using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class navMenuIG : MonoBehaviour
{
    public GameObject MenuInGame;

    //*****BOTONES Y UI****
    public List<Button> ArrBMenus = new List<Button>();
    public List<GameObject> ArrMenus = new List<GameObject>();

    public List<Button> ArrBTutorialesComer = new List<Button>();
    public List<Button> ArrBTutorialesBeber = new List<Button>();
    public List<Button> ArrBTutorialesDormir = new List<Button>();
    public List<GameObject> ArrMenusTutorialesComer = new List<GameObject>();
    public List<GameObject> ArrMenusTutorialesBeber = new List<GameObject>();
    public List<GameObject> ArrMenusTutorialesDormir = new List<GameObject>();

    public List<Button> ArrBObjetos = new List<Button>();
    public List<GameObject> ArrMenusObjetos = new List<GameObject>();

    public List<Button> ArrBPulsera = new List<Button>();
    public List<GameObject> ArrMenusPulsera = new List<GameObject>();

    public List<Button> ArrBEnemigos = new List<Button>();
    public List<GameObject> ArrMenusEnemigos = new List<GameObject>();

    public List<Button> ArrBNotasMundo = new List<Button>();
    public List<Button> ArrBNotasPersonas = new List<Button>();
    public List<List<Button>> ArrDNotas = new List<List<Button>>();
    public List<GameObject> ArrMenusNotasMundo = new List<GameObject>();
    public List<GameObject> ArrMenusNotasPersonas = new List<GameObject>();

    public List<Button> ArrBOpciones = new List<Button>();
    public List<GameObject> ArrMenusOpciones = new List<GameObject>();


    public List<List<Button>> ArrBotones = new List<List<Button>>();
    public List<List<Button>> ArrBotonesNotas = new List<List<Button>>();
    public List<List<Button>> ArrBotonesTutoriales = new List<List<Button>>();
    public List<List<GameObject>> ArrSubmenus = new List<List<GameObject>>();

    public List<GameObject> desplegables;

    public static int idBotonActivo = 0;
    public static int idSubBotonActivo = 0;

    private Vector3 botonGrande = new Vector3(1.1f, 1.1f, 1);
    private Vector3 botonNormal = new Vector3(1, 1, 1);

    // *****VARIABLES****
    public static int life = 100, money = 100, adrenaline = 100;    // Aqui todos los ints (ESTOS NO ACTUALIZAN LOS DE LA ARRAY, para eso la función de SINCRONIZAR)
    public static int[] intsActuales = { life, money, adrenaline };   // AQUI LOS QUE SE TENGAN QUE GUARDAR



    // Start is called before the first frame update
    void Start()
    {
        ArrBotonesNotas.Add(ArrBNotasMundo);
        ArrBotonesNotas.Add(ArrBNotasPersonas);

        ArrBotonesTutoriales.Add(ArrBTutorialesComer);
        ArrBotonesTutoriales.Add(ArrBTutorialesBeber);
        ArrBotonesTutoriales.Add(ArrBTutorialesDormir);

        ArrBotones.Add(null);
        ArrBotones.Add(ArrBObjetos);
        ArrBotones.Add(ArrBPulsera);
        ArrBotones.Add(ArrBEnemigos);
        ArrBotones.Add(null);
        ArrBotones.Add(ArrBOpciones);

        ArrSubmenus.Add(ArrMenusTutorialesComer);
        ArrSubmenus.Add(ArrMenusTutorialesBeber);
        ArrSubmenus.Add(ArrMenusTutorialesDormir);
        ArrSubmenus.Add(ArrMenusObjetos);
        ArrSubmenus.Add(ArrMenusPulsera);
        ArrSubmenus.Add(ArrMenusEnemigos);
        ArrSubmenus.Add(ArrMenusNotasMundo);
        ArrSubmenus.Add(ArrMenusNotasPersonas);
        ArrSubmenus.Add(ArrMenusOpciones);

        // *****TEST MEMORIA*****
        CargarVariables();

        life++;
        money += 5;
        adrenaline += 100;
        Sincronizar();

        GuardarVariables();

        AsignarListeners();
        MenuInGame.SetActive(false);


        // *****BOTONES*****
        DesactivarMenus();
        idBotonActivo = scrMemoria.memIdBotonActivo;    // esto podria ir dentro de la array tambien
        ArrBMenus[idBotonActivo].onClick.Invoke();   // Activa solo el último boton que se pulsó
        Debug.Log("void start");
    }



    // Update is called once per frame
    void Update()
    {

    }

    void AsignarListeners()
    {
        // Asignar listeners para los botones del menu
        for (int i = 0; i < ArrBMenus.Count; i++)
        {
            int boton = i;
            ArrBMenus[i].onClick.AddListener(() => BotonPulsado(boton, 0));
        }

        // Asignar listeners para los botones de los submenus
        /*for (int i = 0; i < ArrBObjetos.Count; i++)
        {
            int boton = i;
            ArrBObjetos[i].onClick.AddListener(() => BotonPulsado(boton, 1));
        }*/
        /*for (int i = 0; i < ArrBPulsera.Count; i++)
        {
            int boton = i;
            ArrBPulsera[i].onClick.AddListener(() => BotonPulsado(boton, 2));
        }*/
        for (int i = 0; i < ArrBEnemigos.Count; i++)
        {
            int boton = i;
            ArrBEnemigos[i].onClick.AddListener(() => BotonPulsado(boton, 1));
        }
        for (int i = 0; i < ArrBOpciones.Count; i++)
        {
            int boton = i;
            ArrBOpciones[i].onClick.AddListener(() => BotonPulsado(boton, 2));
        }
        for (int i = 0; i < ArrBNotasMundo.Count; i++)
        {
            int boton = i;
            ArrBNotasMundo[i].onClick.AddListener(() => BotonPulsado(boton, 5));
        }
        for (int i = 0; i < ArrBNotasPersonas.Count; i++)
        {
            int boton = i;
            ArrBNotasPersonas[i].onClick.AddListener(() => BotonPulsado(boton, 6));
        }
        for (int i = 0; i < ArrBTutorialesComer.Count; i++)
        {
            int boton = i;
            ArrBTutorialesComer[i].onClick.AddListener(() => BotonPulsado(boton, 7));
        }
        for (int i = 0; i < ArrBTutorialesBeber.Count; i++)
        {
            int boton = i;
            ArrBTutorialesBeber[i].onClick.AddListener(() => BotonPulsado(boton, 8));
        }
        for (int i = 0; i < ArrBTutorialesDormir.Count; i++)
        {
            int boton = i;
            ArrBTutorialesDormir[i].onClick.AddListener(() => BotonPulsado(boton, 9));
        }
    }

    public void BotonPulsado(int idBotonPulsado, int tipoBoton)
    {
        switch (tipoBoton)
        {

            case 0:
                ManejarBoton(idBotonPulsado, ArrMenus, ArrBMenus, false);
                break;
            case 2:
                ManejarBoton(idBotonPulsado, ArrMenusOpciones, ArrBOpciones, true);
                break;
            case 6:
                ManejarBoton(idBotonPulsado, ArrMenusNotasPersonas, ArrBNotasPersonas, true);
                break;
            case 5:
                ManejarBoton(idBotonPulsado, ArrMenusNotasMundo, ArrBNotasMundo, true);
                break;
            case 1:
                ManejarBoton(idBotonPulsado, ArrMenusEnemigos, ArrBEnemigos, true);
                break;
            case 4:
                ManejarBoton(idBotonPulsado, ArrMenusPulsera, ArrBPulsera, true);
                break;
            case 3:
                ManejarBoton(idBotonPulsado, ArrMenusObjetos, ArrBObjetos, true);
                break;
            case 7:
                ManejarBoton(idBotonPulsado, ArrMenusTutorialesComer, ArrBTutorialesComer, true);
                break;
            case 8:
                ManejarBoton(idBotonPulsado, ArrMenusTutorialesBeber, ArrBTutorialesBeber, true);
                break;
            case 9:
                ManejarBoton(idBotonPulsado, ArrMenusTutorialesDormir, ArrBTutorialesDormir, true);
                break;
        }
    }
    private void ManejarBoton(int idBotonPulsado, List<GameObject> arrayElementos, List<Button> arrayBotones, bool subMenu)
    {
        if (subMenu == false)
        {
            DesactivarMenus();
            //arrayBotones[idBotonActivo].transform.localScale = botonNormal;

            idBotonActivo = idBotonPulsado;

            scrMemoria.GuardarIdBotonActivo();

            arrayElementos[idBotonActivo].SetActive(true);  
            switch (idBotonActivo)
            {
                case 0:
                    DesplegablePulsado(2);
                    break;
                case 4:
                    DesplegablePulsado(0);
                    break;
                default:
                    idSubBotonActivo = 0;
                    ArrBotones[idBotonActivo][idSubBotonActivo].onClick.Invoke();
                    break;
            }
            
            /*if (idBotonActivo == 0)
            {
                DesplegablePulsado(2);
            }
            if (idBotonActivo == 4)
            {
                DesplegablePulsado(0);
            }
            else
            {
                idSubBotonActivo = 0;
                ArrBotones[idBotonActivo][idSubBotonActivo].onClick.Invoke();
            }*/
            //arrayBotones[idBotonActivo].transform.localScale = botonGrande;
        }
        if (subMenu == true)
        {
            DesactivarSubmenus();
            //arrayBotones[idBotonActivo].transform.localScale = botonNormal;

            idSubBotonActivo = idBotonPulsado;

            scrMemoria.GuardarIdBotonActivo();

            arrayElementos[idSubBotonActivo].SetActive(true);
            //arrayBotones[idBotonActivo].transform.localScale = botonGrande;
        }
    }

    void DesactivarMenus()
    {
        for (int i = 0; i < ArrMenus.Count; i++)   // Desactivar todos los textos de todos los subapartados (hacer funcion)
        {
            ArrMenus[i].SetActive(false);
        }
    }
    void DesactivarSubmenus()
    {
        foreach (List<GameObject> menu in ArrSubmenus)
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

    public void DesplegablePulsado(int index)
    {
        // Cierra todos los menús primero
        CerrarDesplegables();

        // Activa solo el menú seleccionado
        switch (index)
        {
            case 0:
                desplegables[index].SetActive(true);
                ArrBotonesNotas[0][0].onClick.Invoke();
                break;
            case 1:
                desplegables[index].SetActive(true);
                ArrBotonesNotas[1][0].onClick.Invoke();
                break;
            case 2:
                desplegables[index].SetActive(true);
                ArrBotonesTutoriales[0][0].onClick.Invoke();
                break;
            case 3:
                desplegables[index].SetActive(true);
                ArrBotonesTutoriales[1][0].onClick.Invoke();
                break;
            case 4:
                desplegables[index].SetActive(true);
                ArrBotonesTutoriales[2][0].onClick.Invoke();
                break;
        }
    }
    private void CerrarDesplegables()
    {
        foreach (GameObject desplegable in desplegables)
        {
            desplegable.SetActive(false);
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