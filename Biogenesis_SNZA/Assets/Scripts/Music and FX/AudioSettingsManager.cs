using UnityEngine;

public class AudioSettingsManager : MonoBehaviour
{
    [Range(0f, 1f)] public float musicVol = 0.1f;
    [Range(0f, 1f)] public float effectsVol = 1f;

    public AudioSource[] musicSources;
    public AudioSource[] effectsSources;

    public static AudioSettingsManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Update()
    {
        foreach (AudioSource music in musicSources)
            music.volume = musicVol;

        foreach (AudioSource fx in effectsSources)
            fx.volume = effectsVol;
    }
}