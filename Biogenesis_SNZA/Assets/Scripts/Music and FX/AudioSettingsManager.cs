using System.Collections;
using UnityEngine;

public class AudioSettingsManager : MonoBehaviour
{
    public static AudioSettingsManager instance;

    [Range(0f, 1f)] public float masterVol = 1f;

    [Range(0f, 1f)][SerializeField] private float _musicVolRaw = 1f;
    [Range(0f, 1f)][SerializeField] private float _effectsVolRaw = 1f;

    public float masterVolumeSave;
    public static float musicVol => instance._musicVolRaw * instance.masterVol;
    public static float effectsVol => instance._effectsVolRaw * instance.masterVol;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // prevent duplicates
            return;
        }

        instance = this;
    }

    private void FixedUpdate()
    {
            //musicVol = musicVol * masterVol;
            //effectsVol = effectsVol * masterVol;
    }
}