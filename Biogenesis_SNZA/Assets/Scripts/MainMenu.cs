using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "GameScene"; // Replace with your actual scene name
    public GameObject settingsPanel;
    private void Start()
    {
        settingsPanel.SetActive(false);
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
        // Call your load logic here
        Debug.Log("Loading Game...");
        // For example:
        // SaveController_Script.LoadGame(); (if you expose it statically or trigger through another object)
        SceneManager.LoadScene("levelDesign 3");
    }

    public void ToggleSettings()
    {
        Debug.Log("Open Settings");
        // You can activate a settings panel here
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        // settingsPanel.SetActive(true);
    }

    public void OpenCredits()
    {
        Debug.Log("Open Credits");
        // Activate a credits panel or load a credits scene
        // SceneManager.LoadScene("Credits");
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
