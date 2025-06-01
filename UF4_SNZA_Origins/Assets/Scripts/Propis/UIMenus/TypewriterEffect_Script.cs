using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Burst.CompilerServices;

public class TypewriterEffect_Script : MonoBehaviour
{
    public GameObject buttonMainMenu;
    public GameObject buttonMainMenuImage;
    [SerializeField] private float delayShowButton;
    private TMP_Text textBox;

    private int currentVisibleCharacterIndex;
    private Coroutine typewriterCoroutine;

    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _puntapartDelay;

    [SerializeField] private float charactersPerSecond = 20f;
    [SerializeField] private float puntapartDelay = 0.5f;

    [SerializeField] AudioClip voiceSound;
    private AudioSource audioSource;

    //skipping logic
    public bool estaSaltando {  get; private set; }
    public GameObject textoSkip;

    private void Awake()
    {
        textBox = GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();
        _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        _puntapartDelay = new WaitForSeconds(puntapartDelay);
        //skipDelay = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));
    }

    // Start is called before the first frame update
    void Start()
    {
        SetText(textBox.text);
        buttonMainMenuImage.SetActive(false);
        buttonMainMenu.SetActive(false);
        textoSkip.SetActive(false);
        Invoke("ActivateTextoSkip", 3f);
        //Invoke("ActivateButton", delayShowButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textBox.maxVisibleCharacters != textBox.textInfo.characterCount - 1) { Skip(); }
        }
    }
    public void SetText(string text)
    {
        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }
        textBox.text = text;
        textBox.maxVisibleCharacters = 0;
        currentVisibleCharacterIndex = 0;
        typewriterCoroutine = StartCoroutine(routine:Typewriter());
    }
    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = textBox.textInfo;
        while (currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {
            char character = textInfo.characterInfo[currentVisibleCharacterIndex].character;
            textBox.maxVisibleCharacters++;
            audioSource.clip = voiceSound;
            audioSource.Play();
            if (!estaSaltando && character == '.' || character == ',')
            {
                yield return _puntapartDelay;
            }
            else
            {
                yield return _simpleDelay;
            }
            currentVisibleCharacterIndex++;
            if (textBox.maxVisibleCharacters == textBox.textInfo.characterCount)
            {
                ActivateButton();
            }
        }
    }
    void Skip()
    {
        if (estaSaltando || textoSkip == null) { return; }
        estaSaltando = true;
        StopCoroutine(typewriterCoroutine);
        ActivateButton();
        textBox.maxVisibleCharacters = textBox.textInfo.characterCount;
    }
    /*private IEnumerator SkipSpeedupReset()
    {
        yield return new WaitUntil(() => textBox.maxVisibleCharacters == textBox.textInfo.characterCount - 1);
        estaSaltando = false;
    }*/
    void ActivateTextoSkip()
    {
        textoSkip.SetActive(true);
    }
    void ActivateButton()
    {
        buttonMainMenuImage.SetActive(true);
        buttonMainMenu.SetActive(true);
    }
}
