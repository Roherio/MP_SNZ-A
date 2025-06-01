using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ferho serializable vol dir que podem empaquetar i desempaquetar dades en format de text
[System.Serializable]
public class SaveData_Script
{
    //definim les dades qe voldrem guardar
    public Vector3 playerPosition;
    public float lifeLiora;
    public int municionLiora;
    public bool[] inventorySaveData;

    public string mapBoundary; //aqui guardarem els limits de la camera

    public bool poderKhione;
    public bool poderRumo;
}