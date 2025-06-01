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
            municionLiora = GameControl_Script.municion,

            //GUARDAR DATO DE SI TIENES PODER KHIONE Y RUMO
            poderKhione = EventsManager_Script.poderKhione,
            poderRumo = EventsManager_Script.poderRumo,
        };
        //transformar la variable saveData a un tipus Json
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }
    public void LoadGame()
    {
        CameraFading fade = Camera.main.GetComponent<CameraFading>();
        fade.FadeInSlow();
        if (File.Exists(saveLocation))
        {
            
            //llegir el arxiu de TXT per carregarlo
            SaveData_Script saveData = JsonUtility.FromJson<SaveData_Script>(File.ReadAllText(saveLocation));
            //
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            //HealthBar_Script.currentHealth = saveData.lifeLiora;
            GameControl_Script.lifeLiora = saveData.lifeLiora;
            GameControl_Script.municion = saveData.municionLiora;

            //Cargar Poderes
            EventsManager_Script.poderKhione = saveData.poderKhione;
            EventsManager_Script.poderRumo = saveData.poderRumo;

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