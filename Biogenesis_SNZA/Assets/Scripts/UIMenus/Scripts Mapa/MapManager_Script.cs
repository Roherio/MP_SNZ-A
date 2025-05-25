using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager_Script : MonoBehaviour
{
    [SerializeField] private Image[] maps;
    [SerializeField] private bool[] activeMapsIndex;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        activeMapsIndex = new bool[maps.Length];
        for (int i = 0; i < activeMapsIndex.Length; i++)
        {
            activeMapsIndex[i] = false;
        }
        for (int i = 0; i < maps.Length; i++)
        {
            if (activeMapsIndex[i])
            {
                maps[i].enabled = true;
            }
            else
            {
                maps[i].enabled = false;
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
        maps[index].enabled = true;
    }
    public void ActivateAllMaps()
    {
        for (int i = 0; i < maps.Length; i++)
        {
            activeMapsIndex[i] = true;
            maps[i].enabled = true;
        }
    }
}