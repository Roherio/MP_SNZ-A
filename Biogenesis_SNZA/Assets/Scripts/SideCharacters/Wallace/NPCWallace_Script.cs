using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NPCWallace_Script : MonoBehaviour, IInteractable_Script
{
    public AudioClip startConversationSFX;
    //dialogueData1 es primera vez
    //dialogueData2 es que vayas a hablar con Khione
    //dialogueData3 es cuando has hablado ya con Khione
    public NPCDialogue_Script dialogueData1;
    public NPCDialogue_Script dialogueData2;
    public NPCDialogue_Script dialogueData3;

    private DialogueController dialogueUI;

    public string currentDialogue = "dialogueData1";

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    private void Start()
    {
        dialogueUI = DialogueController.Instance;
    }
    //variable que nom�s serveix perqu� Khione hagi de dir una vegada m�nim el dialeg en el que t'explica quines peces necessita, ja que sin� podries arribar amb les dues peces a l'inventari i te les accepta
    void Update()
    {
        if (isDialogueActive)
        {
            GameControl_Script.isPausedDialogue = true;
        }
        if (currentDialogue == "dialogueData2" && EventsManager_Script.habladoKhione1vez)
        {
            currentDialogue = "dialogueData3";
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
        AudioSource.PlayClipAtPoint(startConversationSFX, transform.position);
        if (currentDialogue == "dialogueData3")
        {
            isDialogueActive = true;
            dialogueIndex = 0;

            dialogueUI.SetNPCInfo(dialogueData3.npcName, dialogueData3.npcPortrait);
            dialogueUI.ShowDialogueUI(true);

            StartCoroutine(TypeLine());
        }
        if (currentDialogue == "dialogueData2")
        {
            isDialogueActive = true;
            dialogueIndex = 0;

            dialogueUI.SetNPCInfo(dialogueData2.npcName, dialogueData2.npcPortrait);
            dialogueUI.ShowDialogueUI(true);

            StartCoroutine(TypeLine());
        }
        if (currentDialogue == "dialogueData1")
        {
            isDialogueActive = true;
            dialogueIndex = 0;

            dialogueUI.SetNPCInfo(dialogueData1.npcName, dialogueData1.npcPortrait);
            dialogueUI.ShowDialogueUI(true);

            StartCoroutine(TypeLine());
        }
    }
    void NextLine()
    {
        if (currentDialogue == "dialogueData3")
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueUI.SetDialogueText(dialogueData3.dialogueLines[dialogueIndex]);
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
                dialogueUI.SetDialogueText(dialogueData2.dialogueLines[dialogueIndex]);
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
            }
        }
        if (currentDialogue == "dialogueData1")
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueUI.SetDialogueText(dialogueData1.dialogueLines[dialogueIndex]);
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
        dialogueUI.SetDialogueText("");
        if (currentDialogue == "dialogueData3")
        {
            foreach (char letter in dialogueData3.dialogueLines[dialogueIndex])
            {
                dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);
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
                dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);
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
                dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);
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
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);
        GameControl_Script.isPausedDialogue = false;
    }
}