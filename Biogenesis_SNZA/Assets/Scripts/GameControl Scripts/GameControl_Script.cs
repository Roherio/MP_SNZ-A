using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl_Script : MonoBehaviour
{
    public static GameControl_Script instance;
    
    //logica liora
    public static float lifeLiora = 500f;
    public static float maxLife = 500f;
    public static float adrenalineLiora = 0f;
    public static int moneyLiora = 0;

    //logica items consumibles
    public static int cristalizadores = 0;
    public static int cuero = 0;
    public static int viales = 0;

    //logica progreso SNZAs OJO QUE VAN AL REVES, ESTO SE DEBE A PODER CONTAR ENEMIGOS RESTANTES EN EL MENÚ
    /*public static bool snzaCangrejoConseguida = true;
    public static float progresoSNZAJabali = 150f;
    public static bool snzaJabaliConseguida = false;
    public static float progresoSNZASecretario = 150f;
    public static bool snzaSecretarioConseguida = false;*/

    //logica menus
    public static bool isPaused = false;
    public static bool isPausedDialogue = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

        //si no existeix un SaveFile, farem que la vida de la liora s'inicialitzi sent la màxima. Del contrari, l'agafa del save file. farem el mateix per totes les variables que cal guardar del gameControl.
        if (!FindObjectOfType<SaveController_Script>().HasSaveFile())
        {
            lifeLiora = maxLife;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //------------------------------------ESTO HARA FALTA PASARLO PUNTUALMENTE AL MOMENTO EN EL QUE GASTES EL SINTETIZADOR CON ABRAXAS
        if (cristalizadores <= 0)
        {
            BotonesActivos_Script.bCristalizador = false;
            SNZAProgressJabali_Script.bTachadoCristalizadorJabali = false;
            SNZAProgressSecretario_Script.bTachadoCristalizadorSecretario = false;
        }
        //------------------------------------ESTO HARA FALTA PASARLO PUNTUALMENTE AL MOMENTO EN EL QUE GASTES LOS ITEMS CON KHIONE para hacerte una poción
        if (cuero <= 0)
        {
            BotonesActivos_Script.bCuero = false;
        }
        if (viales <= 0)
        {
            BotonesActivos_Script.bVial = false;
        }
    }
}