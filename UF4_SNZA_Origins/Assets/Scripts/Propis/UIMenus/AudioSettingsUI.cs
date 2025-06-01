using UnityEngine;
using UnityEngine.UI;

//PROPI
//Aqui nomes utilitzem el script per juntar el AudioSettingsManager amb un element de la UI amb sliders
public class AudioSettingsUI : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider effectsSlider;

    private void Start()
    {
        // VALORS INICIALS AudioSettingsManager
        masterSlider.value = AudioSettingsManager.instance.masterVol;
        musicSlider.value = AudioSettingsManager.instance.GetMusicRaw();
        effectsSlider.value = AudioSettingsManager.instance.GetEffectsRaw();

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