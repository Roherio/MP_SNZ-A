using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NPC_Script : MonoBehaviour, IInteractable_Script
{
    //Script MIXTE, adaptant el que ensenya a un tutorial per incloure un dialeg amb un personatge. Canal de Youtube: Game Code Library. Link al vídeo: https://www.youtube.com/watch?v=eSH9mzcMRqw

    //script que serveix per reproduir tot el diàleg amb un personatge.

    public NPCDialogue_Script dialogueData; //d'aquest script recollim totes les dades del diàleg en questio (nom, línies...)
    private DialogueController dialogueUI;//d'aquest script recollim on mostrarho (el panell de la UI, el marc de la foto del personatge...)

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    private void Start()
    {
        //cridem la instància
        dialogueUI = DialogueController.Instance;
    }
    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null || (PauseController_Script.isGamePaused && !isDialogueActive)) { return; }
        //en el cas d'estar reproduint-se el dialeg ja, fem NextLine(), sino fem StartDialogue()
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
        isDialogueActive = true;
        dialogueIndex = 0;

        dialogueUI.SetNPCInfo(dialogueData.npcName, dialogueData.npcPortrait);
        dialogueUI.ShowDialogueUI(true);
        GameControl_Script.isPausedDialogue = true;

        DisplayCurrentLine();
    }
    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            //si hi ha una altra linia, escriula
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }
    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueUI.SetDialogueText("");
        //per cada char dintre de la dialogueLine, el reproduim a pantalla sumant aquella lletra
        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }
    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }
    //funció per acabar el diàleg, fent-nos acabar totes les rutines i desactivant gameObjects del dialeg com pot ser el panell, la imatge del personatge i el seu nom (dialogueUI.ShowDialogueUI(false)).
    void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);
        GameControl_Script.isPausedDialogue = false;
    }
}