using UnityEngine;

public class AudioSettingsManager : MonoBehaviour
{
    public static AudioSettingsManager instance;

    [Range(0f, 1f)] public float musicVol = 1f;
    [Range(0f, 1f)] public float effectsVol = 1f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // prevent duplicates
            return;
        }

        instance = this;
    }
}