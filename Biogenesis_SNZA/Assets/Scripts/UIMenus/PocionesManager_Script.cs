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
        /*if (Input.GetKeyDown(KeyCode.H))
        {
            UsePotion();
        }*/
        if (Input.GetKeyDown(KeyCode.R))
        {
            RefillAllPotions();
        }
    }
    public void Heal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UsePotion();
        }
    }
    public void UsePotion()
    {
        for (int i = pociones.Length - 1; i >= 0; i--)
        {
            Pociones_Script pocionesScript = pociones[i].GetComponent<Pociones_Script>();
            if (pocionesScript.pocionActiva)
            {
                GameControl_Script.lifeLiora += 20f;
                pocionesScript.PotionUsed();
                break;
            }
        }
    }
    public void RefillAllPotions()
    {
        foreach (GameObject potion in pociones)
        {
            potion.GetComponent<Pociones_Script>().PotionRecovered();
        }
    }
}
