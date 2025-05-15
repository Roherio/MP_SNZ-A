using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl_Script : MonoBehaviour
{
    public static GameControl_Script instance;
    
    //logica liora
    public static float lifeLiora = 100f;
    public static float maxLife = 100f;
    public static float adrenalineLiora = 0f;
    public static int moneyLiora = 0;

    //logica items consumibles
    public static int cristalizadores = 0;
    public static int cuero = 0;
    public static int viales = 0;

    //logica progreso SNZAs
    public static float progresoSNZAJabali = 0f;
    //public static int progresoSNZASecretario = 0;
    public static bool snzaJabaliConseguida = false;
    //public static bool snzaSecretarioConseguida = false;

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
        /*if (SaveController_Script.saveLocation == null)
        {
            lifeLiora = maxLife;
        }*/
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}