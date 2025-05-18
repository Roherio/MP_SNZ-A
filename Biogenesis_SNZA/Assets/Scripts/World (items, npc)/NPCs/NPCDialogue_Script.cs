using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPCDialogue")]
public class NPCDialogue_Script : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public bool[] endDialogueLines; //marca que assenyala quines linies acaben el dialeg en ser una branching option
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;
    //public AudioClip voiceSound;
    //public float voicePitch = 1f;

    public DialogueChoice_Script[] choices;
}

[System.Serializable]
public class DialogueChoice_Script
{
    public int dialogueIndex; //linea de dialeg on apareixen les opcions
    public string[] choices; //les opcions a escollir
    public int[] nextDialogueIndexes; //les seguents linies segons la opció escollida
}