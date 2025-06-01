using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//Script que utilitzem per la UI in-game, especificament per el volum, i controls
//PROPI
public class Menu_Script : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject controlesPanel;
    public GameObject volumenPanel;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        controlesPanel.SetActive(false);
        GameControl_Script.isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            return;
        }
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
    public void ToggleVolune()
    {
        volumenPanel.SetActive(!volumenPanel.activeSelf);
    }
}