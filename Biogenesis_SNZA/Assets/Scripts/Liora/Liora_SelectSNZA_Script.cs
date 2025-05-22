using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Liora_Attack_Script;

public class Liora_SelectSNZA_Script : MonoBehaviour
{
    public static Liora_SelectSNZA_Script Instance;

    public Image attackSlotImage;
    public Image parrySlotImage;

    private Sprite selectedSNZASprite = null;
    private string selectedSNZAType = null;

    private void Awake()
    {
        if (Instance == null) { Instance = this;}
        else { Destroy(gameObject); }
    }
    public void SelectSNZA(Image snzaSprite, string nombreSNZA)
    {
        selectedSNZASprite = snzaSprite.sprite;
        selectedSNZAType = nombreSNZA;
    }
    public void AsignarSNZAalSlot(Image imagenEnSlot)
    {
        if (selectedSNZASprite != null)
        {
            imagenEnSlot.sprite = selectedSNZASprite;
            Color visibleColor = Color.white;
            visibleColor.a = 1f;
            imagenEnSlot.color = visibleColor;
            
            if (imagenEnSlot.gameObject.name == "ButtonAttack")
            {
                if (selectedSNZAType == "Cangrejo")
                {
                    Liora_Attack_Script.currentAttackType = snzaAttackType.CANGREJO;
                }
                if (selectedSNZAType == "Jabali")
                {
                    Liora_Attack_Script.currentAttackType = snzaAttackType.JABALI;
                }
                if (selectedSNZAType == "Secretario")
                {
                    Liora_Attack_Script.currentAttackType = snzaAttackType.SECRETARIO;
                }
            }
            else if (imagenEnSlot.gameObject.name == "ButtonParry")
            {
                if (selectedSNZAType == "Cangrejo")
                {
                    Liora_Attack_Script.currentParryType = snzaParryType.CANGREJO;
                }
                if (selectedSNZAType == "Jabali")
                {
                    Liora_Attack_Script.currentParryType = snzaParryType.JABALI;
                }
                if (selectedSNZAType == "Secretario")
                {
                    Liora_Attack_Script.currentParryType = snzaParryType.SECRETARIO;
                }
            }
            selectedSNZASprite = null;
            selectedSNZAType = null;
        }
    }
    /*public void SelectAttackCrab()
    {
        Liora_Attack_Script.currentAttackType = snzaAttackType.CANGREJO;
    }
    public void SelectParryCrab()
    {
        Liora_Attack_Script.currentParryType = snzaParryType.CANGREJO;
    }
    public void SelectAttackBoar()
    {
        Liora_Attack_Script.currentAttackType = snzaAttackType.JABALI;
    }
    public void SelectParryBoar()
    {
        Liora_Attack_Script.currentParryType = snzaParryType.JABALI;
    }*/
}