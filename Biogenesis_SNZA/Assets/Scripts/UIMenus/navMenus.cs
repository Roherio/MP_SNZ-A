using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class navMenus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
}
