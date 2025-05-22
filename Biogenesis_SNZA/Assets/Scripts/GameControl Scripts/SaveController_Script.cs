using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveController_Script : MonoBehaviour
{
    private static string saveLocation;
  
    // Start is called before the first frame update
    void Awake()
    {

        //determinem on es guarda la save
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        LoadGame();
    }

    public static void SaveGame()
    {
        SaveData_Script saveData = new SaveData_Script
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            mapBoundary = FindObjectOfType <CinemachineConfiner>().m_BoundingShape2D.gameObject.name,
            lifeLiora = GameControl_Script.lifeLiora,
            //GUARDAR DATO DE QUE OBJETOS TIENES, TODOS LOS OBJETOS QUE SON TRUE EN TU INVENTARIO Y DOCUMENTOS, PERSONAJES CONOCIDOS...
            cristalizadores = GameControl_Script.cristalizadores,
            cuero = GameControl_Script.cuero,
            viales = GameControl_Script.viales,

            //si tienes este item en el inventario
            barraKhione = EventsManager_Script.barraKhione,
            muelleKhione = EventsManager_Script.muelleKhione,
            taponesRumo = EventsManager_Script.taponesRumo,
            mantaRumo = EventsManager_Script.mantaRumo,
            
            //guardar si has obtenido el item alguna vez
            barraObtenida = EventsManager_Script.barraObtenida,
            muelleObtenido = EventsManager_Script.muelleObtenido,
            taponesObtenidos = EventsManager_Script .taponesObtenidos,
            mantaObtenida = EventsManager_Script.mantaObtenida,
            //GUARDAR DATO DE SI TIENES PODER KHIONE Y RUMO
            poderKhione = EventsManager_Script.poderKhione,
            poderRumo = EventsManager_Script.poderRumo,

            //GUARDAR CON LOS NPCS QUE HAS HABLADO
            habladoDioltas1vez = EventsManager_Script.habladoDioltas1vez,
            habladoKhione1vez = EventsManager_Script.habladoKhione1vez,
            habladoWallace1vez = EventsManager_Script.habladoWallace1vez
        };
        //transformar la variable saveData a un tipus Json
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }
    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            //llegir el arxiu de TXT per carregarlo
            SaveData_Script saveData = JsonUtility.FromJson<SaveData_Script>(File.ReadAllText(saveLocation));
            //
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            //HealthBar_Script.currentHealth = saveData.lifeLiora;
            GameControl_Script.lifeLiora = saveData.lifeLiora;
            //Cargar objetos inventario
            GameControl_Script.cristalizadores = saveData.cristalizadores;
            GameControl_Script.cuero = saveData.cuero;
            GameControl_Script.viales = saveData.viales;

            EventsManager_Script.barraKhione = saveData.barraKhione;
            EventsManager_Script.muelleKhione = saveData.muelleKhione;
            EventsManager_Script.taponesRumo = saveData.taponesRumo;
            EventsManager_Script.mantaRumo = saveData.mantaRumo;

            EventsManager_Script.barraObtenida = saveData.barraObtenida;
            EventsManager_Script.muelleObtenido = saveData.muelleObtenido;
            EventsManager_Script.taponesObtenidos = saveData.taponesObtenidos;
            EventsManager_Script.mantaObtenida = saveData.mantaObtenida;
            //Cargar Poderes
            EventsManager_Script.poderKhione = saveData.poderKhione;
            EventsManager_Script.poderRumo = saveData.poderRumo;
            //personajes hablados
            EventsManager_Script.habladoDioltas1vez = saveData.habladoDioltas1vez;
            EventsManager_Script.habladoKhione1vez = saveData.habladoKhione1vez;
            EventsManager_Script.habladoWallace1vez = saveData.habladoWallace1vez;
            FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D = GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();
             
        }
        else
        {
            SaveGame();
        }
    }
    public bool HasSaveFile()
    {
        return File.Exists(saveLocation);
    }
}