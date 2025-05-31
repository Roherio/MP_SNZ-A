using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl_Script : MonoBehaviour
{
    public static GameControl_Script instance;
    
    //logica liora
    public static float lifeLiora = 100f;
    public static float maxLife = 100f;

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
    private void Start()
    {
        CameraFading fade = Camera.main.GetComponent<CameraFading>();
        if (fade != null)
        {
            // Start the fade-in with an empty callback
            fade.FadeInSlow(() => { });
        }
        else
        {
            Debug.LogWarning("CameraFading component not found on the Main Camera.");
        }
    }
    void Update()
    {

    }
}