using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPCDialogue")]
public class NPCDialogue_Script : ScriptableObject
{
    //Script EXTERN, que ens permet utilitzar un sistema de diàleg per la introducció a la partida amb el personatge que apareix. Canal de Youtube: Game Code Library. Link al vídeo: https://www.youtube.com/watch?v=eSH9mzcMRqw
    //script que serveix per determinar quines variables necessitarà el scriptable object que creem. Un scriptable object ens serveix com a plantilla creada a través de script pels diferents personatges que poguéssim crear (el seu nom, la seva imatge ampliada, les seves línies de diàleg...)
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;
}