using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PocionesManager_Script : MonoBehaviour
{
    public GameObject pocionesPanel;
    public GameObject pocionPrefab;
    public static int pocionesCount = 3;
    public GameObject[] pociones;
    
    // Start is called before the first frame update
    void Start()
    {
        pociones = new GameObject[pocionesCount];
        for (int i = 0; i < pocionesCount; i++)
        {
            GameObject pocion = Instantiate(pocionPrefab, pocionesPanel.transform);
            pocion.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            pociones[i] = pocion; //guardar las pociones en la array de gameObjects
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}