using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ferho serializable vol dir que podem empaquetar i desempaquetar dades en format de text
[System.Serializable]
public class SaveData_Script
{
    //definim les dades qe voldrem guardar
    /*public static float playerPositionX;
    public static float playerPositionY;*/
    public Vector3 playerPosition;
    public float lifeLiora;
    //public string mapBoundary; //aqui guardarem els limits de la camera quan els apliquem
    //Inventory saving també
}
