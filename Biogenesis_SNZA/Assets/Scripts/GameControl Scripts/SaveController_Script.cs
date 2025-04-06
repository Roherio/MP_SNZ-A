using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveController_Script : MonoBehaviour
{
    private string saveLocation;
    // Start is called before the first frame update
    void Awake()
    {
        //determinem on es guarda la save
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        LoadGame();
    }

    public void SaveGame()
    {
        SaveData_Script saveData = new SaveData_Script
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            //mapBoundary = FindObjectOfType<CinemachineConfiner<().m_BoundingShape2D.gameObject.name
            lifeLiora = GameControl_Script.lifeLiora
            //lifeLiora = HealthBar_Script.currentHealth
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
            /*
             * FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D = GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();
             */
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