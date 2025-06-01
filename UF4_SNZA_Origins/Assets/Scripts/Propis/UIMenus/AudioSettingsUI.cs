using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider effectsSlider;

    private void Start()
    {
        // Set initial values from AudioSettingsManager
        masterSlider.value = AudioSettingsManager.instance.masterVol;
        musicSlider.value = AudioSettingsManager.instance.GetMusicRaw();
        effectsSlider.value = AudioSettingsManager.instance.GetEffectsRaw();

        // Add listeners to sliders
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        effectsSlider.onValueChanged.AddListener(SetEffectsVolume);
    }

    public void SetMasterVolume(float value)
    {
        AudioSettingsManager.instance.masterVol = value;
    }

    public void SetMusicVolume(float value)
    {
        AudioSettingsManager.instance.SetMusicRaw(value);
    }

    public void SetEffectsVolume(float value)
    {
        AudioSettingsManager.instance.SetEffectsRaw(value);
    }
}