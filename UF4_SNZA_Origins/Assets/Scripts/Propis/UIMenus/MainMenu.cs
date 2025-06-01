using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Script PROPI
    //Script que permet controlar les funcions que fan els botons del menú principal o botons de transició d'una escena a altra.
    public string gameSceneName = "GameScene"; //utilitzar el nom de l'escena en qüestió
    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public travelCredits creditsScript;
    private void Start()
    {
        //settingsPanel.SetActive(false);
        //creditsPanel.SetActive(false);
    }
    public void StartIntro()
    {
        SceneManager.LoadScene("TransicioInici");
    }
    public void PlayGame()
    {
        string saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

        if (File.Exists(saveLocation))
        {
            File.Delete(saveLocation);
            Debug.Log("partida guardada borrada");

        }
        SceneManager.LoadScene("levelDesign 3");
    }

    public void LoadGame()
    {
        Debug.Log("Cargando...");
        SceneManager.LoadScene("levelDesign 3");
    }

    public void ToggleSettings()
    {
        Debug.Log("abriendo Settings");
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
        Debug.Log("abriendoCreditos");
        creditsScript.resetCredits();
        creditsScript.activeCredits = !creditsScript.activeCredits;
    }
    public void OpenWebLink()
    {
        Application.OpenURL("https://www.instagram.com/level11_studio/");
    }
    public void BackToMainMenu()
    {
        Debug.Log("volviendo a menu principal");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        Debug.Log("saliendo...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
