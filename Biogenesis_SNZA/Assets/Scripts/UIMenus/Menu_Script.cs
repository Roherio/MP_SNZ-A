using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Menu_Script : MonoBehaviour
{
    public GameObject menuCanvas;
    public PlayerInput playerInput;
    ////////logica pestanyas
    public Image[] pestanyasImages;
    public GameObject[] paginas;
    public int numeroPestanya = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        menuCanvas.SetActive(false);
        GameControl_Script.isPaused = false;
        ActivarPestanya(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
        if (menuCanvas.activeSelf)
        {
            GameControl_Script.isPaused = true;
        }
        if (!menuCanvas.activeSelf)
        {
            GameControl_Script.isPaused = false;
        }
        UpdatePestanyas();
        ActivarPestanya(numeroPestanya);
    }
    //pestanyas con input system
    /*public void Pestanyas(InputAction.CallbackContext context)
    {
        print("tas cambiando de pestanya");
        inputPestanyas = context.ReadValue<Vector2>().x;
    }*/
    public void ToggleMenu()
    {
        menuCanvas.SetActive(!menuCanvas.activeSelf);
    }
    public void ActivarPestanya(int tabNumber)
    {
        for (int i = 0; i < paginas.Length; i++)
        {
            paginas[i].SetActive(false);
            pestanyasImages[i].color = Color.grey;
        }
        paginas[tabNumber].SetActive(true);
        pestanyasImages[tabNumber].color = Color.white;
    }
    public void UpdatePestanyas()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            numeroPestanya--;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            numeroPestanya++;
        }
        if (numeroPestanya < 0)
        {
            numeroPestanya = 3;
        }
        if (numeroPestanya > 3)
        {
            numeroPestanya = 0;
        }
    }
}