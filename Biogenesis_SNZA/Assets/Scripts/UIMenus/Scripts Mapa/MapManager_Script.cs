using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager_Script : MonoBehaviour
{
    [SerializeField] private GameObject[] maps;
    public static bool[] activeMapsIndex;
    [SerializeField] private GameObject[] masksLiora;
    private bool[] activeMasksIndex;

    public static MapManager_Script instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        activeMapsIndex = new bool[maps.Length];
        activeMasksIndex = new bool[masksLiora.Length];
        for (int i = 0; i < activeMapsIndex.Length; i++)
        {
            activeMapsIndex[i] = false;
        }
        for (int i = 0; i < activeMasksIndex.Length; i++)
        {
            activeMasksIndex[i] = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (EventsManager_Script.allMapsActive)
        {
            ActivateAllMaps();
        }
        //activar o desactivar mapas y mascaras en funcion de true o false. si allMapsActive es true, se activaran todos los elementos del mapa.
        for (int i = 0; i < maps.Length; i++)
        {
            if (activeMapsIndex[i])
            {
                maps[i].SetActive(true);
            }
            else
            {
                maps[i].SetActive(false);
            }
        }
        for (int i = 0; i < masksLiora.Length; i++)
        {
            if (activeMasksIndex[i])
            {
                masksLiora[i].SetActive(true);
            }
            else
            {
                masksLiora [i].SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateMap(int index)
    {
        activeMapsIndex[index] = true;
        maps[index].SetActive(true);
        if (activeMapsIndex[26] && activeMapsIndex[7])
        {
            activeMapsIndex[36] = true;
            maps[36].SetActive(true);
        }
    }
    public void ActivateAllMaps()
    {
        for (int i = 0; i < maps.Length; i++)
        {
            activeMapsIndex[i] = true;
            maps[i].SetActive(true);
        }
    }
    public void ActivateMask(int index)
    {
        for (int i = 0; i < masksLiora.Length; i++)
        {
            activeMasksIndex[i] = false;
            masksLiora[i].SetActive(false);
        }
        activeMasksIndex[index] = true;
        masksLiora[index].SetActive(true);
    }
    public void DeactivateMask(int index)
    {
        activeMasksIndex[index] = false;
        masksLiora[index].SetActive(false);
    }
}