using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NPCWallace_Script : MonoBehaviour, IInteractable_Script
{
    //dialogueData1 es primera vez
    //dialogueData2 es despues de hablar la primera vez
    //dialogueData3 es cuando has conseguido alguno de los objetos pero no todos
    //dialogueData4 es cuando acabas de conseguir los dos objetos y te da la habilidad
    //dialogueData5 es despues de darte la habilidad
    public NPCDialogue_Script dialogueData1;
    public NPCDialogue_Script dialogueData2;
    public NPCDialogue_Script dialogueData3;
    public NPCDialogue_Script dialogueData4;
    public NPCDialogue_Script dialogueData5;
    
    public string currentDialogue = "dialogueData1";
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    //variable que només serveix perquè Khione hagi de dir una vegada mínim el dialeg en el que t'explica quines peces necessita, ja que sinó podries arribar amb les dues peces a l'inventari i te les accepta
    void Update()
    {
        if (currentDialogue == "dialogueData4" && EventsManager_Script.habladoKhione1vez)
        {
            currentDialogue = "dialogueData5";
        }
    }
    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (PauseController_Script.isGamePaused && !isDialogueActive) { return; }
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
        if (currentDialogue == "dialogueData5")
        {
            isDialogueActive = true;
            dialogueIndex = 0;

            nameText.SetText(dialogueData5.npcName);
            portraitImage.sprite = dialogueData5.npcPortrait;

            dialoguePanel.SetActive(true);
            PauseController_Script.SetPause(true);

            StartCoroutine(TypeLine());
        }
        if (currentDialogue == "dialogueData4")
        {
            isDialogueActive = true;
            dialogueIndex = 0;

            nameText.SetText(dialogueData4.npcName);
            portraitImage.sprite = dialogueData4.npcPortrait;

            dialoguePanel.SetActive(true);
            PauseController_Script.SetPause(true);

            StartCoroutine(TypeLine());
        }
        if (currentDialogue == "dialogueData3")
        {
            isDialogueActive = true;
            dialogueIndex = 0;

            nameText.SetText(dialogueData3.npcName);
            portraitImage.sprite = dialogueData3.npcPortrait;

            dialoguePanel.SetActive(true);
            PauseController_Script.SetPause(true);

            StartCoroutine(TypeLine());
        }
        if (currentDialogue == "dialogueData2")
        {
            isDialogueActive = true;
            dialogueIndex = 0;

            nameText.SetText(dialogueData2.npcName);
            portraitImage.sprite = dialogueData2.npcPortrait;

            dialoguePanel.SetActive(true);
            PauseController_Script.SetPause(true);

            StartCoroutine(TypeLine());
        }
        if (currentDialogue == "dialogueData1")
        {
            isDialogueActive = true;
            dialogueIndex = 0;

            nameText.SetText(dialogueData1.npcName);
            portraitImage.sprite = dialogueData1.npcPortrait;

            dialoguePanel.SetActive(true);
            PauseController_Script.SetPause(true);

            StartCoroutine(TypeLine());
        }
    }
    void NextLine()
    {
        if (currentDialogue == "dialogueData5")
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.SetText(dialogueData5.dialogueLines[dialogueIndex]);
                isTyping = false;
            }
            else if (++dialogueIndex < dialogueData5.dialogueLines.Length)
            {
                //si hi ha una altra linia, escriula
                StartCoroutine(TypeLine());
            }
            else
            {
                EndDialogue();
            }
        }
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
                currentDialogue = "dialogueData4";
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
                currentDialogue = "dialogueData2";
            }
        }
    }
    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");
        if (currentDialogue == "dialogueData5")
        {
            foreach (char letter in dialogueData5.dialogueLines[dialogueIndex])
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(dialogueData5.typingSpeed);
            }

            isTyping = false;

            if (dialogueData5.autoProgressLines.Length > dialogueIndex && dialogueData5.autoProgressLines[dialogueIndex])
            {
                yield return new WaitForSeconds(dialogueData5.autoProgressDelay);
                NextLine();
            }
        }
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
        PauseController_Script.SetPause(false);
    }
}