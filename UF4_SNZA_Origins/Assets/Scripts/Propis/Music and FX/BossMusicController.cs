using UnityEngine;


//PROPI
//Aquest Script controla la musica del Boss, ja que aquesta te una intro, un loop i un final, per tant funciona de forma diferent.
[RequireComponent(typeof(AudioSource))] //ixo hop fiquem per asegurar-nos que te el audiosource, encara que tambe ho hem fet al Start
public class BossMusicController : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip introClip;
    public AudioClip loopClip;
    public AudioClip endClip;

    private bool hasStarted = false;
    private bool hasStartedLoop = false;
    private bool hasPlayedEnd = false;
    public bool bossDefeated = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.volume = AudioSettingsManager.musicVol;
        // Comencem el loop
        if (hasStarted && !audioSource.isPlaying && !hasStartedLoop && !bossDefeated)
        {
            audioSource.clip = loopClip;
            audioSource.loop = true;
            audioSource.Play();
            hasStartedLoop = true;
        }
        if (Liora_StateMachine_Script.isDying == true)
        {
            
            audioSource.Stop();
            hasStarted = false;
            hasStartedLoop = false;
            hasPlayedEnd = false;
        }

        // fes play de l'ultima part
        if (bossDefeated && !hasPlayedEnd)
        {
            PlayEndMusic();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasStarted && other.CompareTag("Player"))
        {
            StartMusic();
        }
    }

    public void StartMusic()
    {
        hasStarted = true;
        audioSource.clip = introClip;
        audioSource.loop = false;
        audioSource.Play();
        print("introBoss");
    }

    public void TriggerBossDefeat()
    {
        bossDefeated = true;
    }

    private void PlayEndMusic()
    {
        hasPlayedEnd = true;
        audioSource.loop = false;
        audioSource.clip = endClip;
        audioSource.Play();
    }
}