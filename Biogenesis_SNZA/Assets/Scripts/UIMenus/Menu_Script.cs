using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Menu_Script : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject pausePanel;
    public GameObject controlesPanel;

    public PlayerInput playerInput;
    ////////logica pestanyas
    public Image[] pestanyasImages;
    public GameObject[] paginas;
    public int numeroPestanya = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        menuCanvas.SetActive(false);
        pausePanel.SetActive(false);
        controlesPanel.SetActive(false);
        GameControl_Script.isPaused = false;
        ActivarPestanya(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            return;
        }
        if (!pausePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ToggleMenu();
            }
            UpdatePestanyas();
            ActivarPestanya(numeroPestanya);
        }
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
        GameControl_Script.isPaused = menuCanvas.activeSelf;
    }
    public void TogglePause()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        controlesPanel.SetActive(false);
        PauseController_Script.SetPause(pausePanel.activeSelf);
    }
    public void ToggleAjustes()
    {
        controlesPanel.SetActive(!controlesPanel.activeSelf);
    }

    public void ActivarPestanya(int tabNumber)
    {
        for (int i = 0; i < paginas.Length; i++)
        {
            paginas[i].SetActive(false);
            pestanyasImages[i].color = new Color(240f / 255f, 226f / 255f, 190f / 255f);
        }
        paginas[tabNumber].SetActive(true);
        pestanyasImages[tabNumber].color = new Color(187f / 255f, 141f / 255f, 100f / 255f);
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