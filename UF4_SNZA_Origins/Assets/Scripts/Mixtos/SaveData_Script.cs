using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ferho serializable vol dir que podem empaquetar i desempaquetar dades en format de text
[System.Serializable]
public class SaveData_Script
{
    //SCRIPT MIXTE, extret de un tutorial i adaptat al nostre projecte. Canal de Youtube: Game Code Library. Link al vídeo: https://www.youtube.com/watch?v=rDZztBWGMIs

    //aquest script serveix per definir QUINES variables volem guardar a la nostre saveData, serveix com a esquema. el guardat en sí s'executa al script SaveController_Script

    //definim les dades qe voldrem guardar
    public Vector3 playerPosition;
    public float lifeLiora;
    public int municionLiora;
    public bool[] inventorySaveData;

    public string mapBoundary; //aqui guardarem els limits de la camera

}