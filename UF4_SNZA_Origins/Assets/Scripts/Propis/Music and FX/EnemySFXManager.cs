using UnityEngine;


//PROPI
//Aquest script només controla el so dels efectes dels Enemics (encara que es troba a altres elements on ens ha resultat util, aixi no haviem de crear un script identic)

public class EnemySFXManager : MonoBehaviour
{
    public AudioSource audioSource;

    private void Awake()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (AudioSettingsManager.instance != null)
        {
            audioSource.volume = AudioSettingsManager.effectsVol;
        }
    }
}
