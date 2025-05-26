using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "GameScene"; // Replace with your actual scene name
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
            Debug.Log("Save file deleted.");

        }
        SceneManager.LoadScene("levelDesign 3");
    }

    public void LoadGame()
    {
        Debug.Log("Loading Game...");
        SceneManager.LoadScene("levelDesign 3");
    }

    public void ToggleSettings()
    {
        Debug.Log("Open Settings");
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
        Debug.Log("Open Credits");
        creditsScript.resetCredits();
        creditsScript.activeCredits = !creditsScript.activeCredits;
    }

    public void BackToMainMenu()
    {
        Debug.Log("Going to MainMenu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        Debug.Log("Exiting...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
