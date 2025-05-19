using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "GameScene"; // Replace with your actual scene name

    public void PlayGame()
    {
        string saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

        if (File.Exists(saveLocation))
        {
            File.Delete(saveLocation);
            Debug.Log("Save file deleted.");

        }
        SceneManager.LoadScene("levelDesign 2");
    }

    public void LoadGame()
    {
        // Call your load logic here
        Debug.Log("Loading Game...");
        // For example:
        // SaveController_Script.LoadGame(); (if you expose it statically or trigger through another object)
        SceneManager.LoadScene("levelDesign 2");
    }

    public void OpenSettings()
    {
        Debug.Log("Open Settings");
        // You can activate a settings panel here
        // settingsPanel.SetActive(true);
    }

    public void OpenCredits()
    {
        Debug.Log("Open Credits");
        // Activate a credits panel or load a credits scene
        // SceneManager.LoadScene("Credits");
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
