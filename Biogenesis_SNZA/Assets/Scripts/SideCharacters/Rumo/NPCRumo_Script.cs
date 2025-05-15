using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NPCRumo_Script : MonoBehaviour, IInteractable_Script
{
    //dialogueData1 es primera vez, se repite hasta que consigues tapones
    //dialogueData2 es despues de conseguir tapones
    //dialogueData3 se repite hasta que consigues manta
    //dialogueData4 es cuando acabas de conseguir los dos objetos y te da la habilidad
    public NPCDialogue_Script dialogueData1;
    public NPCDialogue_Script dialogueData2;
    public NPCDialogue_Script dialogueData3;
    public NPCDialogue_Script dialogueData4;
    
    public string currentDialogue = "dialogueData1";
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    void Update()
    {
        if (isDialogueActive)
        {
            GameControl_Script.isPausedDialogue = true;
        }
        if (currentDialogue == "dialogueData3")
        {
            if (EventsManager_Script.mantaRumo)
            {
                currentDialogue = "dialogueData4";
            }
        }
        if (currentDialogue == "dialogueData1")
        {
            if (EventsManager_Script.taponesRumo)
            {
                currentDialogue = "dialogueData2";
            }
        }
    }
    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (GameControl_Script.isPaused && !isDialogueActive) { return; }
        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }
    void StartDialogue()
    {
        if (currentDialogue == "dialogueData4")
        {
            isDialogueActive = true;
            dialogueIndex = 0;

            nameText.SetText(dialogueData4.npcName);
            portraitImage.sprite = dialogueData4.npcPortrait;

            dialoguePanel.SetActive(true);
            StartCoroutine(TypeLine());
        }
        if (currentDialogue == "dialogueData3")
        {
            isDialogueActive = true;
            dialogueIndex = 0;

            nameText.SetText(dialogueData3.npcName);
            portraitImage.sprite = dialogueData3.npcPortrait;

            dialoguePanel.SetActive(true);
            StartCoroutine(TypeLine());
        }
        if (currentDialogue == "dialogueData2")
        {
            isDialogueActive = true;
            dialogueIndex = 0;

            nameText.SetText(dialogueData2.npcName);
            portraitImage.sprite = dialogueData2.npcPortrait;

            dialoguePanel.SetActive(true);
            StartCoroutine(TypeLine());
        }
        if (currentDialogue == "dialogueData1")
        {
            isDialogueActive = true;
            dialogueIndex = 0;

            nameText.SetText(dialogueData1.npcName);
            portraitImage.sprite = dialogueData1.npcPortrait;

            dialoguePanel.SetActive(true);
            StartCoroutine(TypeLine());
        }
    }
    void NextLine()
    {
        if (currentDialogue == "dialogueData4")
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.SetText(dialogueData4.dialogueLines[dialogueIndex]);
                isTyping = false;
            }
            else if (++dialogueIndex < dialogueData4.dialogueLines.Length)
            {
                //si hi ha una altra linia, escriula
                StartCoroutine(TypeLine());
            }
            else
            {
                EndDialogue();
                //----------------ACONSEGUIM EL PODER DE TRENCAR MURS
                EventsManager_Script.ActivarAnilloRumo();
                EventsManager_Script.DesactivarObjRumo1();
                EventsManager_Script.DesactivarObjRumo2();
                //-----------------------------------------------------------------AQUI CALDRIA FER LA ANIMACIO DE QUE MARXA O ES FA INVISIBLE O LO QUE SEA, I DESPRES FEM DESTROY GAMEOBJECT
            }
        }
        if (currentDialogue == "dialogueData3")
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.SetText(dialogueData3.dialogueLines[dialogueIndex]);
                isTyping = false;
            }
            else if (++dialogueIndex < dialogueData3.dialogueLines.Length)
            {
                //si hi ha una altra linia, escriula
                StartCoroutine(TypeLine());
            }
            else
            {
                EndDialogue();
            }
        }
        if (currentDialogue == "dialogueData2")
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.SetText(dialogueData2.dialogueLines[dialogueIndex]);
                isTyping = false;
            }
            else if (++dialogueIndex < dialogueData2.dialogueLines.Length)
            {
                //si hi ha una altra linia, escriula
                StartCoroutine(TypeLine());
            }
            else
            {
                EndDialogue();
                currentDialogue = "dialogueData3";
            }
        }
        if (currentDialogue == "dialogueData1")
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.SetText(dialogueData1.dialogueLines[dialogueIndex]);
                isTyping = false;
            }
            else if (++dialogueIndex < dialogueData1.dialogueLines.Length)
            {
                //si hi ha una altra linia, escriula
                StartCoroutine(TypeLine());
            }
            else
            {
                EndDialogue();
            }
        }
    }
    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");
        if (currentDialogue == "dialogueData4")
        {
            foreach (char letter in dialogueData4.dialogueLines[dialogueIndex])
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(dialogueData4.typingSpeed);
            }

            isTyping = false;

            if (dialogueData4.autoProgressLines.Length > dialogueIndex && dialogueData4.autoProgressLines[dialogueIndex])
            {
                yield return new WaitForSeconds(dialogueData4.autoProgressDelay);
                NextLine();
            }
        }
        if (currentDialogue == "dialogueData3")
        {
            foreach (char letter in dialogueData3.dialogueLines[dialogueIndex])
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(dialogueData3.typingSpeed);
            }

            isTyping = false;

            if (dialogueData3.autoProgressLines.Length > dialogueIndex && dialogueData3.autoProgressLines[dialogueIndex])
            {
                yield return new WaitForSeconds(dialogueData3.autoProgressDelay);
                NextLine();
            }
        }
        if (currentDialogue == "dialogueData2")
        {
            foreach (char letter in dialogueData2.dialogueLines[dialogueIndex])
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(dialogueData2.typingSpeed);
            }

            isTyping = false;

            if (dialogueData2.autoProgressLines.Length > dialogueIndex && dialogueData2.autoProgressLines[dialogueIndex])
            {
                yield return new WaitForSeconds(dialogueData2.autoProgressDelay);
                NextLine();
            }
        }
        if (currentDialogue == "dialogueData1")
        {
            foreach (char letter in dialogueData1.dialogueLines[dialogueIndex])
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(dialogueData1.typingSpeed);
            }

            isTyping = false;

            if (dialogueData1.autoProgressLines.Length > dialogueIndex && dialogueData1.autoProgressLines[dialogueIndex])
            {
                yield return new WaitForSeconds(dialogueData1.autoProgressDelay);
                NextLine();
            }
        }
    }
    
    void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        GameControl_Script.isPausedDialogue = false;
    }
}