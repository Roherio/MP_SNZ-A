using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NPCKhione_Script : MonoBehaviour, IInteractable_Script
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

    private DialogueController dialogueUI;

    public string currentDialogue = "dialogueData1";

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    //variable que només serveix perquè Khione hagi de dir una vegada mínim el dialeg en el que t'explica quines peces necessita, ja que sinó podries arribar amb les dues peces a l'inventari i te les accepta
    private bool doOnceDialogue2 = false;
    private void Start()
    {
        dialogueUI = DialogueController.Instance;
    }
    void Update()
    {
        if (isDialogueActive)
        {
            GameControl_Script.isPausedDialogue = true;
        }
        if (currentDialogue == "dialogueData2")
        {
            if (doOnceDialogue2 == true)
            {
                if (EventsManager_Script.barraKhione || EventsManager_Script.muelleKhione)
                {
                    currentDialogue = "dialogueData3";
                }
                if (EventsManager_Script.muelleKhione && EventsManager_Script.barraKhione)
                {
                    currentDialogue = "dialogueData4";
                }
            }
        }
        if (currentDialogue == "dialogueData3")
        {
            if (EventsManager_Script.barraKhione && EventsManager_Script.muelleKhione)
            {
                currentDialogue = "dialogueData4";
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
        if (currentDialogue == "dialogueData5")
        {
            isDialogueActive = true;
            dialogueIndex = 0;

            dialogueUI.SetNPCInfo(dialogueData5.npcName, dialogueData5.npcPortrait);
            dialogueUI.ShowDialogueUI(true);

            StartCoroutine(TypeLine());
        }
        if (currentDialogue == "dialogueData4")
        {
            isDialogueActive = true;
            dialogueIndex = 0;

            dialogueUI.SetNPCInfo(dialogueData4.npcName, dialogueData4.npcPortrait);
            dialogueUI.ShowDialogueUI(true);

            StartCoroutine(TypeLine());
        }
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
        if (currentDialogue == "dialogueData5")
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueUI.SetDialogueText(dialogueData5.dialogueLines[dialogueIndex]);
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
                dialogueUI.SetDialogueText(dialogueData4.dialogueLines[dialogueIndex]);
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
                //------------------ACONSEGUIM EL PODER DE ESCALAR ENREDADERAS
                EventsManager_Script.ActivarAnilloKhione();
                EventsManager_Script.DesactivarObjKhione1();
                EventsManager_Script.DesactivarObjKhione2();
                currentDialogue = "dialogueData5";
            }
        }
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
                doOnceDialogue2 = true;
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
                EventsManager_Script.habladoKhione1vez = true;
            }
        }
    }
    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueUI.SetDialogueText("");
        if (currentDialogue == "dialogueData5")
        {
            foreach (char letter in dialogueData5.dialogueLines[dialogueIndex])
            {
                dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);
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
                dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);
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