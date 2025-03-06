using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class navMenus : MonoBehaviour
{

    public GameObject MenuInGame;

    // Start is called before the first frame update
    void Start()
    {
        MenuInGame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CJuego()
    {
        SceneManager.LoadScene("testSimJuego");
    }
    public void CConfig()
    {
        SceneManager.LoadScene("testConfigM");
    }
    public void CCreditos()
    {
        SceneManager.LoadScene("testCreditos");
    }
    public void CMenuP()
    {
        SceneManager.LoadScene("testMPrincipal");
    }
    public void SalirJuego()
    {
        Application.Quit();
    }
    public void CMenuG()
    {
        MenuInGame.SetActive(!MenuInGame.activeSelf);
        if (MenuInGame.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
