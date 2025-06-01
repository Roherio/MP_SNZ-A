using System.Collections;
using UnityEngine;


//PROPI
//Aquest script ens serveix per controlar el volum dels efectes de so i musica, aquests valors els lliguem a un slider que despres podem portar a la UI
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
    public float GetMusicRaw()
    {
        return _musicVolRaw;
    }

    public void SetMusicRaw(float value)
    {
        _musicVolRaw = value;
    }

    public float GetEffectsRaw()
    {
        return _effectsVolRaw;
    }

    public void SetEffectsRaw(float value)
    {
        _effectsVolRaw = value;
    }
}