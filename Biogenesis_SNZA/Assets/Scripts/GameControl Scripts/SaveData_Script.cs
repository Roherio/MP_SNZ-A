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
    public bool[] inventorySaveData;
    //public int money;

    //public string mapBoundary; //aqui guardarem els limits de la camera quan els apliquem

    //Inventory saving també
    public bool barraKhione;
    public bool muelleKhione;
    public bool taponesRumo;
    public bool mantaRumo;
    public bool poderKhione;
    public bool poderRumo;

    //eventosTienda
    public bool habladoKhione1vez = false;
    //SNZAs també
}
