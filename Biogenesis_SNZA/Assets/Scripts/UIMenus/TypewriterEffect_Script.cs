using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    private void Awake()
    {
        textBox = GetComponent<TMP_Text>();
        _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        _puntapartDelay = new WaitForSeconds(puntapartDelay);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetText(textBox.text);
        buttonMainMenuImage.SetActive(false);
        buttonMainMenu.SetActive(false);
        Invoke("ActivateButton", delayShowButton);
    }

    // Update is called once per frame
    void Update()
    {
        
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

            if (character == '.' || character == ',')
            {
                yield return _puntapartDelay;
            }
            else
            {
                yield return _simpleDelay;
            }
            currentVisibleCharacterIndex++;
        }
    }
    void ActivateButton()
    {
        buttonMainMenuImage.SetActive(true);
        buttonMainMenu.SetActive(true);
    }
}
